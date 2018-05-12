using System;
using System.Collections.Generic;
using System.Text;
using XCore.Environment.Shell.Descriptor.Models;
using XCore.Environment.Shell.State;

namespace XCore.Environment.Shell.EntityFrameworkCore
{
    public class ShellDescriptorPO:ShellDescriptor
    {
        public string FeaturesJson { get; set; }
        public string ParametersJson { get; set; }
    }

    public class ShellStatePO:ShellState {
 
    }
}
