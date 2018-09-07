using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OCore.Modules
{
    /// <summary>
    /// When used on a class, it will include the service only
    /// if the specific features are enabled.
    /// 作用与模块的Startup类(直接或间接继承自IStartup接口的类)用来标示只有在启用了特定功能时,该模块才会提供服务。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class RequireFeaturesAttribute : Attribute
    {
        public RequireFeaturesAttribute(string featureName)
        {
            RequiredFeatureNames = new string[] { featureName };
        }

        public RequireFeaturesAttribute(string featureName, params string[] otherFeatureNames)
        {
            var list = new List<string>(otherFeatureNames);
            list.Add(featureName);

            RequiredFeatureNames = list;
        }

        /// <summary>
        /// The names of the required features.获取所需功能的名称。
        /// </summary>
        public IList<string> RequiredFeatureNames { get; }

        public static IList<string> GetRequiredFeatureNamesForType(Type type)
        {
            var attribute = type.GetTypeInfo().GetCustomAttributes<RequireFeaturesAttribute>(false).FirstOrDefault();

            return attribute?.RequiredFeatureNames ?? Array.Empty<string>();
        }
    }
}
