using System;
using System.Collections.Generic;
using System.Text;
using XCore.Modules.Manifest;

namespace XCore.DisplayManagement.Manifest
{
    /// <summary>
    /// Defines a Theme which is a dedicated Module for theming purposes.
    /// If the Theme has only one default feature, it may be defined there.
    /// 定义一个主题，用户标识该模块是一个主题
    /// </summary>

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    public class ThemeAttribute : ModuleAttribute
    {
        public ThemeAttribute()
        {
        }

        public override string Type => "Theme";

        /// <Summary>The base theme if the theme is derived from an existing one.基本主题，如果主题是从现有主题派生的</Summary>
        public string BaseTheme { get; set; }
    }
}
