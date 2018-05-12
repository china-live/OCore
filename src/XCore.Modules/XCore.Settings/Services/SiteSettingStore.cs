using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using XCore.EntityFrameworkCore;
//using AutoMapper;

namespace XCore.Settings.Services
{
    public interface ISiteSettingStore
    {
        void CreateCurrentSiteSettings(SiteSettings entity);
        void UpdateCurrentSiteSettings(SiteSettings siteSettings);

        SiteSettings GetCurrentSiteSettings();
    }

    public class SiteSettingStore : ISiteSettingStore
    {
        public AppDbContext Context { get; private set; }
        private DbSet<SiteSettingsEntity> SiteSettingsEntitySet { get { return Context.Set<SiteSettingsEntity>(); } }
        public bool AutoSaveChanges { get; set; } = true;
        public virtual IQueryable<SiteSettingsEntity> SiteSettingsEntitys
        {
            get { return SiteSettingsEntitySet; }
        }
        /*private readonly IMapper mapper;*/
        public SiteSettingStore(AppDbContext context/*, IMapper mapper*/)
        {
            //_session = session;
            Context = context ?? throw new ArgumentNullException(nameof(context));
            //this.mapper = mapper;
        }

        protected Task SaveChanges()
        {
            return AutoSaveChanges ? Context.SaveChangesAsync() : Task.CompletedTask;
        }

        public void UpdateCurrentSiteSettings(SiteSettings siteSettings)
        {
            if (siteSettings == null)
            {
                throw new ArgumentNullException(nameof(siteSettings));
            }
            var siteSettingsEntity = SiteSettingsEntitys.FirstOrDefault();
            if (siteSettingsEntity != null)
            {
                //siteSettingsEntity.Id = siteSettingsEntity.Id,
                siteSettingsEntity.BaseUrl = siteSettings.BaseUrl;
                siteSettingsEntity.Calendar = siteSettings.Calendar;
                siteSettingsEntity.Culture = siteSettings.Culture;
                siteSettingsEntity.MaxPagedCount = siteSettings.MaxPagedCount;
                siteSettingsEntity.MaxPageSize = siteSettings.MaxPageSize;
                siteSettingsEntity.PageSize = siteSettings.PageSize;
                siteSettingsEntity.SiteName = siteSettings.SiteName;
                siteSettingsEntity.SiteSalt = siteSettings.SiteSalt;
                siteSettingsEntity.SuperUser = siteSettings.SuperUser;
                siteSettingsEntity.TimeZone = siteSettings.TimeZone;
                siteSettingsEntity.UseCdn = siteSettings.UseCdn;
                siteSettingsEntity.ResourceDebugMode = siteSettings.ResourceDebugMode;
                siteSettingsEntity.HomeRoute_Controller = GetControllerByHomeRoute(siteSettings.HomeRoute);
                siteSettingsEntity.HomeRoute_Action = GetActionByHomeRoute(siteSettings.HomeRoute);
                siteSettingsEntity.HomeRoute_Area = GetAreaByHomeRoute(siteSettings.HomeRoute);

                SiteSettingsEntitySet.Update(siteSettingsEntity);
                SaveChanges();
            }
        }

        public void CreateCurrentSiteSettings(SiteSettings siteSettings)
        {
            if (siteSettings == null)
            {
                throw new ArgumentNullException(nameof(siteSettings));
            }

            var siteSettingsEntity = new SiteSettingsEntity()
            {
                BaseUrl = siteSettings.BaseUrl,
                Calendar = siteSettings.Calendar,
                Culture = siteSettings.Culture,
                MaxPagedCount = siteSettings.MaxPagedCount,
                MaxPageSize = siteSettings.MaxPageSize,
                PageSize = siteSettings.PageSize,
                SiteName = siteSettings.SiteName,
                SiteSalt = siteSettings.SiteSalt,
                SuperUser = siteSettings.SuperUser,
                TimeZone = siteSettings.TimeZone,
                UseCdn = siteSettings.UseCdn,
                ResourceDebugMode = siteSettings.ResourceDebugMode,
                HomeRoute_Controller = GetControllerByHomeRoute(siteSettings.HomeRoute),
                HomeRoute_Action = GetActionByHomeRoute(siteSettings.HomeRoute),
                HomeRoute_Area = GetAreaByHomeRoute(siteSettings.HomeRoute)
            };
 
            SiteSettingsEntitySet.Add(siteSettingsEntity);
            SaveChanges();
        }

        public SiteSettings GetCurrentSiteSettings()
        {
            var siteSettingsEntity = SiteSettingsEntitys.AsNoTracking().FirstOrDefault();
            if (siteSettingsEntity != null)
            {
                return new SiteSettings()
                {
                    Id = siteSettingsEntity.Id,
                    BaseUrl = siteSettingsEntity.BaseUrl,
                    Calendar = siteSettingsEntity.Calendar,
                    Culture = siteSettingsEntity.Culture,
                    MaxPagedCount = siteSettingsEntity.MaxPagedCount,
                    MaxPageSize = siteSettingsEntity.MaxPageSize,
                    PageSize = siteSettingsEntity.PageSize,
                    SiteName = siteSettingsEntity.SiteName,
                    SiteSalt = siteSettingsEntity.SiteSalt,
                    SuperUser = siteSettingsEntity.SuperUser,
                    TimeZone = siteSettingsEntity.TimeZone,
                    UseCdn = siteSettingsEntity.UseCdn,
                    ResourceDebugMode = siteSettingsEntity.ResourceDebugMode,
                    HomeRoute = GetHomeRoute(siteSettingsEntity)
                };
            }
            return null;
        }

        private RouteValueDictionary GetHomeRoute(SiteSettingsEntity c)
        {
            var homeRoute = new RouteValueDictionary();
            if (string.IsNullOrWhiteSpace(c.HomeRoute_Action) == false)
                homeRoute.Add("Action", c.HomeRoute_Action);
            if (string.IsNullOrWhiteSpace(c.HomeRoute_Controller) == false)
                homeRoute.Add("Controller", c.HomeRoute_Controller);
            if (string.IsNullOrWhiteSpace(c.HomeRoute_Area) == false)
                homeRoute.Add("Area", c.HomeRoute_Area);
            return homeRoute;
        }

        private string GetControllerByHomeRoute(RouteValueDictionary homeRoute)
        {
            return (homeRoute != null && homeRoute.ContainsKey("Controller")) ? homeRoute.SingleOrDefault(x => x.Key == "Controller").Value?.ToString() : null;
        }
        private string GetActionByHomeRoute(RouteValueDictionary homeRoute)
        {
            return (homeRoute != null && homeRoute.ContainsKey("Action")) ? homeRoute.SingleOrDefault(x => x.Key == "Action").Value?.ToString() : null;
        }
        private string GetAreaByHomeRoute(RouteValueDictionary homeRoute)
        {
            return (homeRoute != null && homeRoute.ContainsKey("Area")) ? homeRoute.SingleOrDefault(x => x.Key == "Area").Value?.ToString() : null;
        }

    }
}
