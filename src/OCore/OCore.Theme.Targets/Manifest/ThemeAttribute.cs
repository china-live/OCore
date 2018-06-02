using System;
using System.Collections.Generic;
using System.Text;
using OCore.Modules.Manifest;

namespace OCore.Theme
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    public class ThemeAttribute : ModuleAttribute
    {
        public ThemeAttribute()
        {
        }

        public override string Type => "Theme";

        ///// <Summary>The base theme if the theme is derived from an existing one.基本主题，如果主题是从现有主题派生的</Summary>
        //public string BaseTheme { get; set; }
    }
}
