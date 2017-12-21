using Microsoft.Extensions.Options;

namespace XCore.Environment.Extensions
{
    /// <summary>
    /// Sets up default options for <see cref="ExtensionExpanderOptions"/>.
    /// 设置默认的<see cref="ExtensionExpanderOptions"/>类型的扩展程序项（）
    /// </summary>
    public class ExtensionExpanderOptionsSetup : ConfigureOptions<ExtensionExpanderOptions>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ExtensionExpanderOptions"/>.
        /// 初始化一个新的<see cref="ExtensionExpanderOptions"/>实例
        /// </summary>
        public ExtensionExpanderOptionsSetup()
            : base(options => { })
        {
        }
    }
}
