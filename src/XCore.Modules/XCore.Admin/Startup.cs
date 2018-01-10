using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using XCore.DisplayManagement;
using XCore.DisplayManagement.Layout;
using XCore.DisplayManagement.Theming;
using XCore.Environment.Cache;
using XCore.Modules;

namespace XCore.Admin
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            //services.AddNavigation();

            services.Configure<MvcOptions>((options) =>
            {
                //options.Filters.Add(typeof(AdminZoneFilter));
                options.Filters.Add(typeof(LayerFilter));
                //options.Filters.Add(typeof(AdminMenuFilter));
            });

            //services.AddScoped<IPermissionProvider, Permissions>();
            services.AddScoped<IThemeSelector, AdminThemeSelector>();
            //services.AddScoped<IAdminThemeService, AdminThemeService>();
        }

        public override void Configure(IApplicationBuilder builder, IRouteBuilder routes, IServiceProvider serviceProvider)
        {
            //routes.MapAreaRoute(
            //    name: "Adming",
            //    areaName: "XCore.Admin",
            //    template: "adming",
            //    defaults: new { controller = "Admin", action = "Index" }
            //);
        }
    }

    public class LayerFilter : IAsyncResultFilter
    {
        private readonly ILayoutAccessor _layoutAccessor;
        //private readonly IContentItemDisplayManager _contentItemDisplayManager;
       // private readonly IUpdateModelAccessor _modelUpdaterAccessor;
        //private readonly IScriptingManager _scriptingManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMemoryCache _memoryCache;
        private readonly ISignal _signal;
        //private readonly ILayerService _layerService;
        //private readonly IShapeFactory _shapeFactory;

        public LayerFilter(
            //ILayerService layerService,
            //IShapeFactory shapeFactory,
            ILayoutAccessor layoutAccessor,
            //IContentItemDisplayManager contentItemDisplayManager,
            //IUpdateModelAccessor modelUpdaterAccessor,
            //IScriptingManager scriptingManager,
            IServiceProvider serviceProvider,
            IMemoryCache memoryCache,
            ISignal signal)
        {
            //_layerService = layerService;
            _layoutAccessor = layoutAccessor;
            //_contentItemDisplayManager = contentItemDisplayManager;
            //_modelUpdaterAccessor = modelUpdaterAccessor;
            //_scriptingManager = scriptingManager;
            _serviceProvider = serviceProvider;
            _memoryCache = memoryCache;
            _signal = signal;
            //_shapeFactory = shapeFactory;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            // Should only run on the front-end for a full view
            if ((context.Result is ViewResult || context.Result is PageResult) && !AdminAttribute.IsApplied(context.HttpContext))
            {
                //var widgets = await _memoryCache.GetOrCreateAsync("OrchardCore.Layers.LayerFilter:AllWidgets", entry =>
                //{
                //    entry.AddExpirationToken(_signal.GetToken(LayerMetadataHandler.LayerChangeToken));
                //    return _layerService.GetLayerWidgetsAsync(x => x.Published);
                //});

                //var layers = (await _layerService.GetLayersAsync()).Layers.ToDictionary(x => x.Name);

                dynamic layout = await _layoutAccessor.GetLayoutAsync();
                var layout2 = layout;
                //var updater = _modelUpdaterAccessor.ModelUpdater;

                //var engine = _scriptingManager.GetScriptingEngine("js");
                //var scope = engine.CreateScope(_scriptingManager.GlobalMethodProviders.SelectMany(x => x.GetMethods()), _serviceProvider);

                //var layersCache = new Dictionary<string, bool>();

                //foreach (var widget in widgets)
                //{
                //    var layer = layers[widget.Layer];

                //    if (layer == null)
                //    {
                //        continue;
                //    }

                //    bool display;
                //    if (!layersCache.TryGetValue(layer.Name, out display))
                //    {
                //        if (String.IsNullOrEmpty(layer.Rule))
                //        {
                //            display = false;
                //        }
                //        else
                //        {
                //            display = Convert.ToBoolean(engine.Evaluate(scope, layer.Rule));
                //        }

                //        layersCache[layer.Rule] = display;
                //    }

                //    if (!display)
                //    {
                //        continue;
                //    }

                //    IShape widgetContent = await _contentItemDisplayManager.BuildDisplayAsync(widget.ContentItem, updater);

                //    widgetContent.Classes.Add("widget");
                //    widgetContent.Classes.Add("widget-" + widget.ContentItem.ContentType.HtmlClassify());

                //    var wrapper = await _shapeFactory.CreateAsync("Widget_Wrapper", new { Widget = widget.ContentItem, Content = widgetContent });
                //    wrapper.Metadata.Alternates.Add("Widget_Wrapper__" + widget.ContentItem.ContentType);

                //    var contentZone = layout.Zones[widget.Zone];
                //    contentZone.Add(wrapper);
                //}

            }

            await next.Invoke();
        }
    }
}
