using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;
using XCore.DisplayManagement.Layout;

namespace XCore.DisplayManagement
{
    /// <summary>
    /// Used to create a dynamic, contextualized Display object to dispatch shape rendering
    /// </summary>
    public interface IDisplayHelperFactory
    {
        dynamic CreateHelper(ViewContext viewContext);
    }

    
}
