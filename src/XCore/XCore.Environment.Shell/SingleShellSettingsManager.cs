using System;
using System.Collections.Generic;
using System.Text;

namespace XCore.Environment.Shell
{
    class SingleShellSettingsManager : IShellSettingsManager
    {
        public IEnumerable<ShellSettings> LoadSettings()
        {
            yield return new ShellSettings
            {
                Name = "Default",
                State = Models.TenantState.Running
            };
        }

        public void SaveSettings(ShellSettings settings)
        {
             
        }
    }
}
