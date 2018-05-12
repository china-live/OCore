using System.Collections.Generic;
using XCore.Features.Models;

namespace XCore.Features.ViewModels 
{
    public class ModulesIndexViewModel 
    {
        public bool InstallModules { get; set; }
        public IEnumerable<ModuleEntry> Modules { get; set; }

        public ModulesIndexOptions Options { get; set; }
        public dynamic Pager { get; set; }
    }

    public class ModulesIndexOptions 
    {
        public string SearchText { get; set; }
    }
}