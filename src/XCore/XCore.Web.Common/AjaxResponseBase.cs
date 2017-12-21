using System.Collections.Generic;

namespace XCore.Web.Common
{
    public abstract class AjaxResponseBase
    {
        public AjaxResponseBase()
        {
            Errors = new List<ErrorInfo>();
        }

        /// <summary>
        /// 此属性可用于重定向用户到指定的URL。
        /// </summary>
        public string TargetUrl { get; set; }

        /// <summary>
        /// 指示操作结果处理成功的状态值，该属性值为false时可以通过<see cref="Errors"/>获取错误的详细信息。
        /// </summary>
        public bool Success
        {
            get
            {
                return Errors.Count == 0;
            }
        }

        /// <summary>
        /// 详细的错误信息（该属性只有在<see cref="Success"/>值为false时才有值）
        /// </summary>
        public List<ErrorInfo> Errors { get; set; }

        /// <summary>
        /// 此属性可用于指示当前用户是否没有执行此请求的权限。
        /// </summary>
        public bool UnAuthorizedRequest { get; set; }

        /// <summary>
        /// Ajax响应的特殊签名，方便在客户端封装处理服务器的响应格式。
        /// </summary>
        public bool __xcore { get; } = true;
    }
}
