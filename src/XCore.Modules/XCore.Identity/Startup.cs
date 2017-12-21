using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.QQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using XCore.Identity.EntityFrameworkCore;
using XCore.Modules;

namespace XCore.Identity
{
    public class Startup : StartupBase
    {
        private readonly IServiceProvider _applicationServices;

        public Startup(IServiceProvider applicationServices,IConfiguration configuration)
        {
            _applicationServices = applicationServices;
            Configuration = configuration;
        }
 
        public IConfiguration Configuration { get; }

        public override void ConfigureServices(IServiceCollection services)
        {
            // 'IAuthenticationSchemeProvider' is already registered at the host level.
            // We need to register it again so it is taken into account at the tenant level.
            services.AddSingleton<IAuthenticationSchemeProvider, AuthenticationSchemeProvider>();


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddCookie(IdentityConstants.ApplicationScheme, o =>
            {
                o.LoginPath = new PathString("/Admin/Home/Login");
                o.LogoutPath = new PathString("/Admin/Home/Logout");
                o.AccessDeniedPath = new PathString("/Admin/Home/Index");
                o.Events = new CookieAuthenticationEvents
                {
                    OnValidatePrincipal = SecurityStampValidator.ValidatePrincipalAsync
                };
            })
            .AddCookie(IdentityConstants.ExternalScheme, o =>
            {
                o.Cookie.Name = IdentityConstants.ExternalScheme;
                o.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            })
            .AddCookie(IdentityConstants.TwoFactorRememberMeScheme,
                o => o.Cookie.Name = IdentityConstants.TwoFactorRememberMeScheme)
            .AddCookie(IdentityConstants.TwoFactorUserIdScheme, o =>
            {
                o.Cookie.Name = IdentityConstants.TwoFactorUserIdScheme;
                o.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            }).AddQQ(qqOptions =>
            {
                qqOptions.AppId = Configuration["Authentication:QQ:AppId"];
                qqOptions.AppKey = Configuration["Authentication:QQ:AppKey"];
            });

            //services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // Identity services
            services.TryAddScoped<IUserValidator<User>, AppUserValidator<User>>();
            services.TryAddScoped<IPasswordValidator<User>, PasswordValidator<User>>();
            services.TryAddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.TryAddScoped<ILookupNormalizer, UpperInvariantLookupNormalizer>();
            services.TryAddScoped<IRoleValidator<Role>, RoleValidator<Role>>();
            // No interface for the error describer so we can add errors without rev'ing the interface
            services.TryAddScoped<IdentityErrorDescriber>();
            services.TryAddScoped<AppIdentityErrorDescriber>();
            services.TryAddScoped<ISecurityStampValidator, SecurityStampValidator<User>>();
            services.TryAddScoped<IUserClaimsPrincipalFactory<User>, UserClaimsPrincipalFactory<User, Role>>();
            services.TryAddScoped<UserManager<User>, AspNetUserManager<User>>();
            services.TryAddScoped<UserActiveManager<User>, UserActiveManager<User>>();
            services.TryAddScoped<SignInManager<User>, AppSignInManager<User>>();
            services.TryAddScoped<RoleManager<Role>, AspNetRoleManager<Role>>();


            //services.Configure<IdentityOptions>(options =>
            //{
            //    // Password settings
            //    options.Password.RequireDigit = true; //必须包含数字
            //    options.Password.RequiredLength = 8;  //密码最小长度
            //    options.Password.RequireNonAlphanumeric = false; //该标志指示密码是否必须包含非字母数字字符
            //    options.Password.RequireUppercase = true;  //必须包含大写字母ASCII字符。
            //    options.Password.RequireLowercase = true; //必须包含小字母ASCII字符。
            //    options.Password.RequiredUniqueChars = 6;   //必须包含的唯一字符的最小数目

            //    // Lockout settings
            //    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            //    options.Lockout.MaxFailedAccessAttempts = 10; //退出之前允许的失败访问尝试的数量，假设已启用锁定。
            //    options.Lockout.AllowedForNewUsers = true;   //是否为新注册的用户启用锁定

            //    // User settings
            //    options.User.RequireUniqueEmail = true; //保持电子邮件唯一

            //    options.SignIn.RequireConfirmedEmail = false;//验证email
            //    options.SignIn.RequireConfirmedPhoneNumber = false;//验证手机号
            //});
            services.Configure<IdentityOptions>(Configuration.GetSection("Identity"));
            services.Configure<XCoreUserOptions>(Configuration.GetSection("XCoreUser"));
            services.AddSingleton<IConfigureOptions<IdentityOptions>, IdentityOptionsSetupService>();

            //var serviceProvider = services.BuildServiceProvider();
            //var dbContext = serviceProvider.GetService<AppDbContext>();
            new IdentityBuilder(typeof(User), typeof(Role), services).
                AddEntityFrameworkStores().
                AddDefaultTokenProviders();
        }

        public override void Configure(IApplicationBuilder app, IRouteBuilder routes, IServiceProvider serviceProvider)
        {
            app.UseAuthentication();
        }
    }


    public class IdentityOptionsSetupService : IConfigureOptions<IdentityOptions>
    {
        private readonly ILogger<IdentityOptionsSetupService> logger;
 
        public IdentityOptionsSetupService(ILogger<IdentityOptionsSetupService> logger)
        {
            this.logger = logger;
        }

        public void Configure(IdentityOptions options)
        {
            this.logger.LogInformation($"Calling first {typeof(IConfigureOptions<IdentityOptions>)} service");
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
        }
    }
}
