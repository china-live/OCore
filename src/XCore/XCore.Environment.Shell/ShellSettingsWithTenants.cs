using System.Linq;

namespace XCore.Environment.Shell
{
    public class ShellSettingsWithTenants : ShellSettings
    {
        public ShellSettingsWithTenants(ShellSettings shellSettings) : base(shellSettings.Configuration)
        {
            Features = shellSettings
                .Configuration
                .Where(xd => xd.Key.StartsWith("Features:")).Select(xa => xa.Value).ToArray();
        }

        public string[] Features { get; set; }
    }
}
