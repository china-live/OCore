using Microsoft.AspNetCore.Http;

namespace OCore.UEditor.Handlers
{
    public class NotSupportedHandler : HandlerBase
    {
        public NotSupportedHandler(HttpContext context)
            : base(context)
        {
        }

        public override void Process()
        {
            WriteJson(new
            {
                state = "action is empty or action not supperted."
            });
        }
    }
}