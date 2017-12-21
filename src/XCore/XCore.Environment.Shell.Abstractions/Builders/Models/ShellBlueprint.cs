using System;
using System.Collections.Generic;
using XCore.Environment.Shell.Descriptor.Models;
using XCore.Environment.Extensions.Features;

namespace XCore.Environment.Shell.Builders.Models
{
    /// <summary>
    /// Contains the information necessary to initialize an IoC container
    /// for a particular tenant. This model is created by the ICompositionStrategy
    /// and is passed into the IShellContainerFactory.
    /// 包含为特定租户初始化IOC容器所必需的信息。
    /// 该模型是由 ICompositionStrategy 创造并传递到 IShellContainerFactory。
    /// </summary>
    public class ShellBlueprint//blueprint 蓝图，设计图; 计划大纲; 
    {
        public ShellSettings Settings { get; set; }
        public ShellDescriptor Descriptor { get; set; }

        public IDictionary<Type, FeatureEntry> Dependencies { get; set; }
    }
}
