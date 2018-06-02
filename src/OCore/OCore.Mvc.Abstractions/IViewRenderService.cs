using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OCore.Mvc
{
    /// <summary>
    /// 该接口实现将View页面输出为字符串
    /// </summary>
    public interface IViewRenderService
    {
        /// <summary>
        /// 将View页面作为字符串输出
        /// </summary>
        /// <param name="viewName">视图页面名称</param>
        /// <param name="model">要传递到页面数据模型</param>
        /// <returns></returns>
        Task<string> RenderToStringAsync(ViewContext context, string viewName, object model);
    }
}
