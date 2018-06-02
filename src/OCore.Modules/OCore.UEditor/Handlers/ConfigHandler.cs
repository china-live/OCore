using Microsoft.AspNetCore.Http;

namespace OCore.UEditor.Handlers
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