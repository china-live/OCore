using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using OCore.Environment.Cache;

namespace OCore.Settings.Services
{
    /// <summary>
    /// Implements <see cref="ISiteService"/> by storing the site as a Content Item.
    /// </summary>
    public class SiteService : ISiteService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ISignal _signal;
        private readonly IServiceProvider _serviceProvider;
        private const string SiteCacheKey = "SiteService";
        //private readonly ISiteSettingStore _siteSettingStore;


        public SiteService(
            ISignal signal,
            IServiceProvider serviceProvider, 
            //ISiteSettingStore siteSettingStore,
            IMemoryCache memoryCache)
        {
            _signal = signal;
            _serviceProvider = serviceProvider;
            _memoryCache = memoryCache;
            //_siteSettingStore = siteSettingStore;
        }

        /// <inheritdoc/>
        public IChangeToken ChangeToken => _signal.GetToken(SiteCacheKey);

        /// <inheritdoc/>
        public async Task<ISite> GetSiteSettingsAsync()
        {
            ISite site;

            if (!_memoryCache.TryGetValue(SiteCacheKey, out site))
            {
                //var session = GetSession();

                site = GetSession().GetCurrentSiteSettings();

                if (site == null)
                {
                    lock (_memoryCache)
                    {
                        if (!_memoryCache.TryGetValue(SiteCacheKey, out site))
                        {
                            site = new SiteSettings
                            {
                                SiteSalt = Guid.NewGuid().ToString("N"),
                                SiteName = "My OCore Project Application",
                                TimeZone = TimeZoneInfo.Local.Id,
                                PageSize = 10,
                                MaxPageSize = 100,
                                MaxPagedCount = 0
                            };

                            //session.Save(site);
                            GetSession().CreateCurrentSiteSettings(site as SiteSettings);
                            _memoryCache.Set(SiteCacheKey, site);
                            _signal.SignalToken(SiteCacheKey);
                        }
                    }
                }
                else
                {
                    _memoryCache.Set(SiteCacheKey, site);
                    _signal.SignalToken(SiteCacheKey);
                }
            }
            await Task.Run(()=> { });
            return site;
        }

        /// <inheritdoc/>
        public async Task UpdateSiteSettingsAsync(ISite site)
        {
            var existing = GetSession().GetCurrentSiteSettings();

            existing.BaseUrl = site.BaseUrl;
            existing.Calendar = site.Calendar;
            existing.Culture = site.Culture;
            existing.HomeRoute = site.HomeRoute;
            existing.MaxPagedCount = site.MaxPagedCount;
            existing.MaxPageSize = site.MaxPageSize;
            existing.PageSize = site.PageSize;
            existing.Properties = site.Properties;
            existing.ResourceDebugMode = site.ResourceDebugMode;
            existing.SiteName = site.SiteName;
            existing.SiteSalt = site.SiteSalt;
            existing.SuperUser = site.SuperUser;
            existing.TimeZone = site.TimeZone;
            existing.UseCdn = site.UseCdn;

            GetSession().UpdateCurrentSiteSettings(existing);

            _memoryCache.Set(SiteCacheKey, site);
            _signal.SignalToken(SiteCacheKey);
            await Task.Run(() => { });
            return;
        }

        private ISiteSettingStore GetSession()
        {
            var httpContextAccessor = _serviceProvider.GetService<IHttpContextAccessor>();
            return httpContextAccessor.HttpContext.RequestServices.GetService<ISiteSettingStore>();
        }
    }
}
