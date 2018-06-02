using Microsoft.AspNetCore.Html;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;
using System.Threading.Tasks;
using OCore.DisplayManagement.Implementation;

namespace OCore.DisplayManagement.Descriptors
{
    public interface IShapeTableManager
    {
        ShapeTable GetShapeTable(string themeId);
    }
    public interface IShapeTableProvider
    {
        void Discover(ShapeTableBuilder builder);
    }

    public interface IShapeTableHarvester : IShapeTableProvider
    {
    }

    /// <summary>
    /// Represents a marker interface for classes that have Shape methods tagged with <see cref="ShapeAttribute"/>. 
    /// </summary>
    public interface IShapeAttributeProvider
    {
    }

    public static partial class ShapeProviderExtensions
    {
        public static IServiceCollection AddShapeAttributes<T>(this IServiceCollection services) where T : class, IShapeAttributeProvider
        {
            services.AddScoped<T>();
            services.AddScoped<IShapeAttributeProvider, T>();

            return services;
        }


        public class ShapeBinding
        {
            public ShapeDescriptor ShapeDescriptor { get; set; }
            public string BindingName { get; set; }
            public string BindingSource { get; set; }
            public Func<DisplayContext, Task<IHtmlContent>> BindingAsync { get; set; }
        }
    }
}
