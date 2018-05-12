using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XCore.DeferredTasks;
using XCore.EntityFrameworkCore;
using XCore.Environment.Extensions;
using XCore.Environment.Shell;
using XCore.Environment.Shell.Builders;
using XCore.Environment.Shell.Descriptor;
using XCore.Environment.Shell.Descriptor.Models;
using XCore.Environment.Shell.Models;
using XCore.Modules;
using XCore.Recipes.Models;
using XCore.Recipes.Services;
using XCore.Setup.Events;

namespace XCore.Setup.Services
{
    public class SetupService : ISetupService
    {
        private readonly ShellSettings _shellSettings;
        private readonly IShellHost _orchardHost;
        private readonly IShellContextFactory _shellContextFactory;
        private readonly ICompositionStrategy _compositionStrategy;
        private readonly IExtensionManager _extensionManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRunningShellTable _runningShellTable;
        private readonly IEnumerable<IRecipeHarvester> _recipeHarvesters;
        private readonly ILogger _logger;
        private readonly IStringLocalizer T;

        private IEnumerable<RecipeDescriptor> _recipes;

        public SetupService(
            ShellSettings shellSettings,
            IShellHost orchardHost,
            IShellContextFactory shellContextFactory,
            ICompositionStrategy compositionStrategy,
            IExtensionManager extensionManager,
            IHttpContextAccessor httpContextAccessor,
            IRunningShellTable runningShellTable,
            IEnumerable<IRecipeHarvester> recipeHarvesters,
            ILogger<SetupService> logger,
            IStringLocalizer<SetupService> stringLocalizer
            )
        {
            _shellSettings = shellSettings;
            _orchardHost = orchardHost;
            _shellContextFactory = shellContextFactory;
            _compositionStrategy = compositionStrategy;
            _extensionManager = extensionManager;
            _httpContextAccessor = httpContextAccessor;
            _runningShellTable = runningShellTable;
            _recipeHarvesters = recipeHarvesters;
            _logger = logger;
            T = stringLocalizer;
        }

        public async Task<IEnumerable<RecipeDescriptor>> GetSetupRecipesAsync()
        {
            if (_recipes == null)
            {
                var recipeCollections = await Task.WhenAll(_recipeHarvesters.Select(x => x.HarvestRecipesAsync()));
                _recipes = recipeCollections.SelectMany(x => x).Where(x => x.IsSetupRecipe).ToArray();
            }

            return _recipes;
        }

        public Task<string> SetupAsync(SetupContext context)
        {
            var initialState = _shellSettings.State;
            try
            {
                return SetupInternalAsync(context);
            }
            catch
            {
                _shellSettings.State = initialState;
                throw;
            }
        }

        public async Task<string> SetupInternalAsync(SetupContext context)
        {
            string executionId="";

            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Running setup for tenant '{0}'.", _shellSettings.Name);
            }

            // Features to enable for Setup
            string[] hardcoded =
            {
                "XCore.Commons",
                "XCore.Features",
                "XCore.Recipes",
                "XCore.Scripting"
            };

            context.EnabledFeatures = hardcoded.Union(context.EnabledFeatures ?? Enumerable.Empty<string>()).Distinct().ToList();

            // Set shell state to "Initializing" so that subsequent HTTP requests are responded to with "Service Unavailable" while Orchard is setting up.
            // 将shell状态设置为"Initializing",以便在稍后设置中有HTTP请求遇到“Service Unavailable”
            _shellSettings.State = TenantState.Initializing;

            var shellSettings = new ShellSettings(_shellSettings.Configuration);

            if (string.IsNullOrEmpty(shellSettings.DatabaseProvider))
            {
                shellSettings.DatabaseProvider = context.DatabaseProvider;
                shellSettings.ConnectionString = context.DatabaseConnectionString;
                shellSettings.TablePrefix = context.DatabaseTablePrefix;
            }

            // Creating a standalone environment based on a "minimum shell descriptor".
            // In theory this environment can be used to resolve any normal components by interface, and those
            // components will exist entirely in isolation - no crossover between the safemode container currently in effect
            // It is used to initialize the database before the recipe is run.

            var shellDescriptor = new ShellDescriptor
            {
                Features = context.EnabledFeatures.Select(id => new ShellFeature { Id = id }).ToList()
            };

            using (var shellContext = await _shellContextFactory.CreateDescribedContextAsync(shellSettings, shellDescriptor))
            {
                using (var scope = shellContext.EnterServiceScope())
                {
 
                    try
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                        //await store.InitializeAsync();
                        //dbContext.Database.Migrate();
                        dbContext.Database.EnsureDeleted();
                        dbContext.Database.EnsureCreated();

                        if (_logger.IsEnabled(LogLevel.Debug))
                        {
                            _logger.LogDebug("租户'{0}'数据库创建成功", _shellSettings.Name);
                        }
                    }
                    catch (Exception e)
                    {
                        // Tables already exist or database was not found

                        // The issue is that the user creation needs the tables to be present,
                        // if the user information is not valid, the next POST will try to recreate the
                        // tables. The tables should be rolled back if one of the steps is invalid,
                        // unless the recipe is executing?

                        _logger.LogError(e, "An error occurred while initializing the datastore.");
                        context.Errors.Add("DatabaseProvider", T["An error occurred while initializing the datastore: {0}", e.Message]);
                        return null;
                    }

                    // Create the "minimum shell descriptor"
                    var shellDescriptorManager = scope.ServiceProvider.GetService<IShellDescriptorManager>();
                    await shellDescriptorManager.UpdateShellDescriptorAsync(0,shellContext.Blueprint.Descriptor.Features,shellContext.Blueprint.Descriptor.Parameters);

                    var deferredTaskEngine = scope.ServiceProvider.GetService<IDeferredTaskEngine>();
                    if (deferredTaskEngine != null && deferredTaskEngine.HasPendingTasks)
                    {
                        var taskContext = new DeferredTaskContext(scope.ServiceProvider);
                        await deferredTaskEngine.ExecuteTasksAsync(taskContext);
                    }
                }

                executionId = Guid.NewGuid().ToString("n");

                // Create a new scope for the recipe thread to prevent race issues with other scoped
                // services from the request.
                using (var scope = shellContext.EnterServiceScope())
                {
                    var recipeExecutor = scope.ServiceProvider.GetService<IRecipeExecutor>();

                    // Right now we run the recipe in the same thread, later use polling from the setup screen
                    // to query the current execution.
                    //await Task.Run(async () =>
                    //{
                    await recipeExecutor.ExecuteAsync(executionId, context.Recipe, new
                    {
                        SiteName = context.SiteName,
                        AdminUsername = context.AdminUsername,
                        AdminEmail = context.AdminEmail,
                        AdminPassword = context.AdminPassword,
                        DatabaseProvider = context.DatabaseProvider,
                        DatabaseConnectionString = context.DatabaseConnectionString,
                        DatabaseTablePrefix = context.DatabaseTablePrefix
                    });
                    //});
                }
            }

            // Reloading the shell context as the recipe  has probably updated its features
            using (var shellContext = await _orchardHost.CreateShellContextAsync(shellSettings))
            {
                if (_logger.IsEnabled(LogLevel.Debug))
                {
                    _logger.LogDebug("跟据Recipes重新加载Features");
                }

                using (var scope = shellContext.EnterServiceScope())
                {
                    var hasErrors = false;

                    void reportError(string key, string message)
                    {
                        hasErrors = true;
                        context.Errors[key] = message;
                    }

                    // Invoke modules to react to the setup event
                    var setupEventHandlers = scope.ServiceProvider.GetServices<ISetupEventHandler>();
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<SetupService>>();

                    await setupEventHandlers.InvokeAsync(x => x.Setup(
                        context.SiteName,
                        context.AdminUsername,
                        context.AdminEmail,
                        context.AdminPassword,
                        context.DatabaseProvider,
                        context.DatabaseConnectionString,
                        context.DatabaseTablePrefix,
                        reportError
                    ), logger);

                    if (hasErrors)
                    {
                        return executionId;
                    }

                    var deferredTaskEngine = scope.ServiceProvider.GetService<IDeferredTaskEngine>();

                    if (deferredTaskEngine != null && deferredTaskEngine.HasPendingTasks)
                    {
                        var taskContext = new DeferredTaskContext(scope.ServiceProvider);
                        await deferredTaskEngine.ExecuteTasksAsync(taskContext);
                    }
                }
            }

            // Update the shell state
            shellSettings.State = TenantState.Running;
            _orchardHost.UpdateShellSettings(shellSettings);
            //await Task.Run(()=> { });
            return executionId;
        }
    }
}