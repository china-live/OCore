using Microsoft.AspNetCore.Html;
using System;
using XCore.DisplayManagement.Shapes;

namespace XCore.DisplayManagement.Implementation
{
    public class ShapeDisplayContext
    {
        public dynamic Shape { get; set; }
        public ShapeMetadata ShapeMetadata { get; set; }
        public IHtmlContent ChildContent { get; set; }
        public DisplayContext DisplayContext { get; set; }
        public IServiceProvider ServiceProvider { get; set; }
    }
}
