using NLog;
using NLog.LayoutRenderers;
using NLog.Web.LayoutRenderers;
using System.Text;
using XCore.Environment.Shell;

namespace XCore.Logging
{
    /// <summary>
    /// Print the tenant name
    /// </summary>
    [LayoutRenderer(LayoutRendererName)]
    public class TenantLayoutRenderer : AspNetLayoutRendererBase
    {
        public const string LayoutRendererName = "XCore-tenant-name";

        protected override void DoAppend(StringBuilder builder, LogEventInfo logEvent)
        {
            var context = HttpContextAccessor.HttpContext;

            // If there is no ShellSettings in the Features then the log is rendered from the Host
            var tenantName = context.Features.Get<ShellSettings>()?.Name ?? "None";
            builder.Append(tenantName);
        }
    }
}
