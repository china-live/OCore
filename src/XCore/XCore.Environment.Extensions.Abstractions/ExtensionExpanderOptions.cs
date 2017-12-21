using System.Collections.Generic;

namespace XCore.Environment.Extensions
{
    /// <summary>
    /// 扩展程序项集
    /// </summary>
    public class ExtensionExpanderOptions
    {
        /// <summary>
        /// 扩展程序项集
        /// </summary>
        public IList<ExtensionExpanderOption> Options { get; } = new List<ExtensionExpanderOption>();
    }

    /// <summary>
    /// 扩展程序项
    /// </summary>
    public class ExtensionExpanderOption
    {
        /// <summary>
        /// 获取、设置扩展程序所在路径
        /// </summary>
        public string SearchPath { get; set; }
    }
}