using Microsoft.Extensions.DependencyInjection;

namespace XCore.Environment.Extensions.Manifests
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加扩展模块的描述文件定义
        /// </summary>
        /// <param name="services"></param>
        /// <param name="definitionName">扩展模块描述文件的名称</param>
        /// <param name="moduleType">扩展模块类型，分为两个类型“module”和“theme”</param>
        /// <returns></returns>
        public static IServiceCollection AddManifestDefinition(
            this IServiceCollection services,
            string definitionName,
            string moduleType)
        {
            return services.Configure<ManifestOptions>(configureOptions: options =>
            {
                var option = new ManifestOption
                {
                    ManifestFileName = definitionName,
                    Type = moduleType
                };

                options.ManifestConfigurations.Add(option);
            });
        }
    }
}
