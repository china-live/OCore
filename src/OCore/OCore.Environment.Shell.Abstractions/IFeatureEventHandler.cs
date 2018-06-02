using OCore.Environment.Extensions.Features;

namespace OCore.Environment.Shell
{
    public interface IFeatureEventHandler
    {
        /// <summary>
        /// 安装功能时发生
        /// </summary>
        /// <param name="feature"></param>
        void Installing(IFeatureInfo feature);
        /// <summary>
        /// 安装功能完成时发生
        /// </summary>
        /// <param name="feature"></param>
        void Installed(IFeatureInfo feature);
        /// <summary>
        /// 启用功能时发生
        /// </summary>
        /// <param name="feature"></param>
        void Enabling(IFeatureInfo feature);
        /// <summary>
        /// 启用功能完成时发生
        /// </summary>
        /// <param name="feature"></param>
        void Enabled(IFeatureInfo feature);
        /// <summary>
        /// 禁用功能时发生
        /// </summary>
        /// <param name="feature"></param>
        void Disabling(IFeatureInfo feature);
        /// <summary>
        /// 禁用功能完成时发生
        /// </summary>
        /// <param name="feature"></param>
        void Disabled(IFeatureInfo feature);
        /// <summary>
        /// 卸载功能时发生
        /// </summary>
        /// <param name="feature"></param>
        void Uninstalling(IFeatureInfo feature);
        /// <summary>
        /// 卸载功能完成时发生
        /// </summary>
        /// <param name="feature"></param>
        void Uninstalled(IFeatureInfo feature);
    }
}
