using System;
using System.Collections.Generic;
using System.Text;
using OCore.Environment.Shell.Descriptor.Models;
using OCore.Environment.Shell.State;

namespace OCore.Environment.Shell.EntityFrameworkCore
{
    public class ShellDescriptorPO:ShellDescriptor
    {
        public string FeaturesJson { get; set; }
        public string ParametersJson { get; set; }
    }

    public class ShellStatePO:ShellState {
 
    }
}
