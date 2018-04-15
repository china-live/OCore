using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XCore.Environment.Extensions;

namespace XCore.DisplayManagement.Theming
{
    public class ThemingViewsFeatureProvider : IApplicationFeatureProvider<ViewsFeature>
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ViewsFeature feature)
        {
            //feature.ViewDescriptors.Add(new CompiledViewDescriptor()
            //{
            //    ExpirationTokens = Array.Empty<IChangeToken>(),
            //    RelativePath = ViewPath.NormalizePath("/_ViewStart" + RazorViewEngine.ViewExtension),
            //    ViewAttribute = new RazorViewAttribute("/_ViewStart" + RazorViewEngine.ViewExtension, typeof(ThemeViewStart)),
            //    IsPrecompiled = true,
            //});

            //feature.ViewDescriptors.Add(new CompiledViewDescriptor()
            //{
            //    ExpirationTokens = Array.Empty<IChangeToken>(),
            //    RelativePath = ViewPath.NormalizePath("/Views/Shared/_Layout" + RazorViewEngine.ViewExtension),
            //    ViewAttribute = new RazorViewAttribute("/Views/Shared/_Layout" + RazorViewEngine.ViewExtension, typeof(ThemeLayout)),
            //    IsPrecompiled = true,
            //});
            feature.ViewDescriptors.Add(new CompiledViewDescriptor()
            {
                ExpirationTokens = Array.Empty<IChangeToken>(),
                RelativePath = ViewPath.NormalizePath("/_ViewStart" + RazorViewEngine.ViewExtension),
                ViewAttribute = new RazorViewAttribute("/_ViewStart" + RazorViewEngine.ViewExtension, typeof(ThemeLayout2)),
                IsPrecompiled = true,
            });
        }
    }

    //public class ThemeViewStart : RazorPage<dynamic>
    //{
    //    public override Task ExecuteAsync()
    //    {
    //        Layout = "~/Views/Shared/_Layout" + RazorViewEngine.ViewExtension;
    //        return Task.CompletedTask;
    //    }
    //}

    //public class ThemeLayout : Razor.RazorPage<dynamic>
    //{
    //    public override async Task ExecuteAsync()
    //    {
    //        var body = RenderLayoutBody();
    //        this.ThemeLayout.Content.Add(body);
    //        Write(await DisplayAsync(ThemeLayout));
    //    }
    //}

    public class ThemeLayout2 : Razor.RazorPage2<dynamic>
    {
        public override Task ExecuteAsync()
        {
            var x = this.Theme;

            //Layout = "~/" + this.GetTheme(x??"") +  "/Views/Shared/_Layout" + RazorViewEngine.ViewExtension;
            return Task.CompletedTask;
        }
    }
}
