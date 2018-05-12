using AutoMapper;
using Microsoft.AspNetCore.Routing;
using System.Linq;
using XCore.Entities;

namespace XCore.Settings
{
    public class SiteSettings : Entity, ISite
    {
        public int Id { get; set; }
        public string BaseUrl { get; set; }
        public string Calendar { get; set; }
        public string Culture { get; set; }
        public int MaxPagedCount { get; set; }
        public int MaxPageSize { get; set; }
        public int PageSize { get; set; }
        public ResourceDebugMode ResourceDebugMode { get; set; }
        public string SiteName { get; set; }
        public string SiteSalt { get; set; }
        public string SuperUser { get; set; }
        public string TimeZone { get; set; }
        public bool UseCdn { get; set; }
        public RouteValueDictionary HomeRoute { get; set; }
    }

    /// <summary>
    /// 方便关系数据库存储
    /// </summary>
    public class SiteSettingsEntity : SiteSettings
    {
        public string HomeRoute_Area { get; set; }
        public string HomeRoute_Controller { get; set; }
        public string HomeRoute_Action { get; set; }
    }

    //public class SiteSettingsProfile : Profile
    //{
    //    public SiteSettingsProfile()
    //    {
    //        CreateMap<SiteSettingsEntity, SiteSettings>()
    //            .ForMember(a => a.HomeRoute, b => b.ResolveUsing(c =>
    //            {
    //                var homeRoute = new RouteValueDictionary();
    //                homeRoute.Add("Action", c.HomeRoute_Action);
    //                homeRoute.Add("Controller", c.HomeRoute_Controller);
    //                homeRoute.Add("Area", c.HomeRoute_Area);
    //                return homeRoute;
    //            }));
    //        CreateMap<SiteSettings, SiteSettingsEntity>()
    //            .ForMember(a => a.HomeRoute_Action, b => b.ResolveUsing(c => c.HomeRoute.Single(x => x.Key == "Action").Value.ToString()))
    //            .ForMember(a => a.HomeRoute_Area, b => b.ResolveUsing(c => c.HomeRoute.Single(x => x.Key == "Area").Value.ToString()))
    //            .ForMember(a => a.HomeRoute_Controller, b => b.ResolveUsing(c => c.HomeRoute.Single(x => x.Key == "Controller").Value.ToString()));
    //    }

    //}
}