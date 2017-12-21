using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace XCore.Mvc.Core
{
    public class ViewRenderService : IViewRenderService
    {
        private readonly IRazorViewEngine _razorViewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;

        public ViewRenderService(IRazorViewEngine razorViewEngine,
            ITempDataProvider tempDataProvider,
            IServiceProvider serviceProvider)
        {
            _razorViewEngine = razorViewEngine;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
        }

        public async Task<string> RenderToStringAsync(ViewContext context, string viewName, object model)
        {
            var viewContext = context;
            var viewResult = _razorViewEngine.FindView(viewContext, viewName, false);

            if (viewResult.Success)
            {
                using (var sw = new StringWriter())
                {
                    var view = viewResult.View;
                    using (view as IDisposable)
                    {
                        var viewContext2 = new ViewContext(viewContext, viewResult.View, viewContext.ViewData, sw);

                        await viewResult.View.RenderAsync(viewContext2);
                        return sw.ToString();
                    }
                }

                //var bufferScope = viewContext.HttpContext.RequestServices.GetRequiredService<IViewBufferScope>();
                //var viewBuffer = new ViewBuffer(bufferScope, viewResult.ViewName, ViewBuffer.PartialViewPageSize);
                //using (var writer = new ViewBufferTextWriter(viewBuffer, viewContext.Writer.Encoding))
                //{
                //    // Forcing synchronous behavior so users don't have to await templates.
                //    var view = viewResult.View;
                //    using (view as IDisposable)
                //    {
                //        var viewContext2 = new ViewContext(viewContext, viewResult.View, viewContext.ViewData, writer);
                //        await viewResult.View.RenderAsync(viewContext2);
                //        return viewBuffer;
                //    }
                //}
            }

            return null;
        }
    }
}


