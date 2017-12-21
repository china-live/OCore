using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XCore.UEditor;
using Microsoft.AspNetCore.Authorization;

namespace XCore.Mvc.Admin.Controllers
{
    [Authorize]
    public class UEditorController : Controller
    {
        private UEditorService ue;
        public UEditorController(UEditorService ue)
        {
            this.ue = ue;
        }
 
        [IgnoreAntiforgeryToken]
        public void Index()
        {
            ue.DoAction(HttpContext);
        }
    }
}
