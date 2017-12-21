using XCore.Entities;

namespace XCore.Settings
{
    public interface ISite : IEntity
    {
        string SiteName { get; set; }
        string SiteSalt { get; set; }
        string SuperUser { get; set; }//超级用户
        string Culture { get; set; }
        string Calendar { get; set; }
        string TimeZone { get; set; }
        ResourceDebugMode ResourceDebugMode { get; set; }
        bool UseCdn { get; set; }
        int PageSize { get; set; }
        int MaxPageSize { get; set; }
        int MaxPagedCount { get; set; }
        string BaseUrl { get; set; }
        //RouteValueDictionary HomeRoute { get; set; }
    }
}
