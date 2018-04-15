using System;
using System.Collections.Generic;
using System.Linq;

namespace XCore.Modules.Manifest
{
    /// <summary>
    /// Defines a Module which is composed of features.定义由功能组成的模块。
    /// If the Module has only one default feature, it may be defined there.如果模块只有一个默认功能，则可以在此处定义。
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    public class ModuleAttribute : FeatureAttribute
    {
        public ModuleAttribute()
        {
        }

        public virtual string Type => "Module";
        public new bool Exists => Id != null;

        /// <Summary>
        /// This identifier is overridden at runtime by the assembly name该标识符在运行时被程序集名称覆盖
        /// </Summary>
        public new string Id { get; internal set; }

        /// <Summary>The name of the developer.开发者的名字</Summary>
        public string Author { get; set; } = String.Empty;

        /// <Summary>The URL for the website of the developer.开发人员的网站URL。</Summary>
        public string Website { get; set; } = String.Empty;

        /// <Summary>The version number in SemVer format.SemVer格式的版本号。</Summary>
        public string Version { get; set; } = "0.0";

        /// <Summary>A list of tags.标签列表</Summary>
        public string[] Tags { get; set; } = Enumerable.Empty<string>().ToArray();

        /// <summary>
        /// 该模块包含的功能列表
        /// </summary>
        public List<FeatureAttribute> Features { get; } = new List<FeatureAttribute>();
    }

    /// <summary>
    /// Defines a Feature in a Module, can be used multiple times.在模块中定义一个功能，可以定义多个
    /// If at least one Feature is defined, the Module default feature is ignored.如果定义了多个功能，则模块默认功能将被忽略
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public class FeatureAttribute : Attribute
    {
        public FeatureAttribute()
        {
        }

        public bool Exists => Id != null;

        /// <Summary>The identifier of the feature.</Summary>
        public string Id { get; set; }

        /// <Summary>
        /// Human-readable name of the feature. If not provided, the identifier will be used.
        /// 为该功能提供一个便于我们自已（人类）阅读的名称，如果没有提供，将使用标识符
        /// </Summary>
        public string Name { get; set; }

        /// <Summary>A brief summary of what the feature does.该功能的简要说明</Summary>
        public string Description { get; set; } = String.Empty;

        /// <Summary>
        /// A list of features that the feature depends on.该功能依赖的其他功能列表。
        /// So that its drivers / handlers are invoked after those of dependencies.
        /// </Summary>
        public string[] Dependencies { get; set; } = Enumerable.Empty<string>().ToArray();

        /// <Summary>
        /// The priority of the feature without breaking the dependencies order.该功能的优先级，该优先级不会破坏依赖项
        /// higher is the priority, later the drivers / handlers are invoked.
        /// </Summary>
        public string Priority { get; set; } = "0";

        /// <Summary>
        /// The group (by category) that the feature belongs.该功能所属的组（按类别）。
        /// If not provided, defaults to 'Uncategorized'.如果没有提供，默认为'Uncategorized'（未分类）。
        /// </Summary>
        public string Category { get; set; }
    }

    /// <summary>
    /// Marks an assembly as a module of a given type, auto generated on building.将程序集标记为指定类型的模块，在构建时自动生成。
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    public class ModuleMarkerAttribute : ModuleAttribute
    {
        private string _type;

        public ModuleMarkerAttribute(string name, string type)
        {
            Name = name;
            _type = type;
        }

        public override string Type => _type;
    }

    /// <summary>
    /// Enlists the package or project name of a referenced module, auto generated on building.设该模块的名称（package或project），以供引用该模块时使用，在构建时自动生成。
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public class ModuleNameAttribute : Attribute
    {
        public ModuleNameAttribute(string name)
        {
            Name = name ?? String.Empty;
        }

        /// <Summary>
        /// The package or project name of the referenced module.获取引用该包或项目时要使用的名称
        /// </Summary>
        public string Name { get; }
    }

    /// <summary>
    /// Maps a module asset to its project location while in debug mode, auto generated on building.在调试模式下将模块资产映射到其项目位置，在构建时自动生成。
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public class ModuleAssetAttribute : Attribute
    {
        public ModuleAssetAttribute(string asset)
        {
            Asset = asset ?? String.Empty;
        }

        /// <Summary>
        /// A module asset in the form of '{ModuleAssetPath}|{ProjectAssetPath}'.模块资产的形式‘{ModuleAssetPath}|{ProjectAssetPath}’
        /// </Summary>
        public string Asset { get; }
    }
}