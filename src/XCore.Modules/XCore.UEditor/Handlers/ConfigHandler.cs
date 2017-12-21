using Microsoft.AspNetCore.Http;

namespace XCore.UEditor.Handlers
{
    public class ConfigHandler : HandlerBase
    {
        public ConfigHandler(HttpContext context) : base(context) { }

        public override void Process()
        {
            WriteJson(Config.Items);
        }
    }
}