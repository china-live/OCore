using Microsoft.AspNetCore.Mvc.Razor;

namespace XCore.Mvc.LocationExpander
{
    public interface IViewLocationExpanderProvider : IViewLocationExpander
    {
        int Priority { get; }
    }
}
