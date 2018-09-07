using System;
using System.Text;

namespace OCore.Modules
{
    /// <summary>
    /// An attribute that can associate a service or component with
    /// a specific feature by its name. This component will only
    /// be used if the feature is enabled.
    /// 可以通过名称将服务或组件与特定功能相关联的属性,该组件仅在启用该功能时才会使用。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class FeatureAttribute : Attribute
    {
        public FeatureAttribute(string featureName)
        {
            FeatureName = featureName;
        }

        /// <summary>
        /// The name of the feaure to assign the component to.
        /// </summary>
        public string FeatureName { get; set; }
    }
}
