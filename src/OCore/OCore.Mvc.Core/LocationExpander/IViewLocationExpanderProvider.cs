using Microsoft.AspNetCore.Mvc.Razor;

namespace OCore.Mvc.LocationExpander
{
    public interface IViewLocationExpanderProvider : IViewLocationExpander
    {
        int Priority { get; }
    }
}
