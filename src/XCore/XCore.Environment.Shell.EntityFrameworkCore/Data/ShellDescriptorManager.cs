using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using XCore.Environment.Shell.Descriptor;
using XCore.Environment.Shell.Descriptor.Models;
using XCore.Modules;

namespace XCore.Environment.Shell.Data
{
    /// <summary>
    /// Implements <see cref="IShellDescriptorManager"/> by providing the list of features store in the database. 
    /// </summary>
    public class ShellDescriptorManager : IShellDescriptorManager
    {
        private readonly ShellSettings _shellSettings;
        private readonly IEnumerable<IShellDescriptorManagerEventHandler> _shellDescriptorManagerEventHandlers;
        private readonly ILogger _logger;
        protected internal IShellDescriptorStore<ShellDescriptor> Store { get; set; }
        protected virtual CancellationToken CancellationToken => CancellationToken.None;

        //private ShellDescriptor _shellDescriptor;

        public ShellDescriptorManager(
            ShellSettings shellSettings,
            IEnumerable<IShellDescriptorManagerEventHandler> shellDescriptorManagerEventHandlers,
            IShellDescriptorStore<ShellDescriptor> store,
            ILogger<ShellDescriptorManager> logger)
        {
            _shellSettings = shellSettings;
            _shellDescriptorManagerEventHandlers = shellDescriptorManagerEventHandlers;
            Store = store;
            _logger = logger;
        }

        public async Task<ShellDescriptor> GetShellDescriptorAsync()
        {
            return await Store.GetShellDescriptorAsync();
        }

        public async Task UpdateShellDescriptorAsync(int priorSerialNumber, IEnumerable<ShellFeature> enabledFeatures, IEnumerable<ShellParameter> parameters)
        {
            var shellDescriptorRecord = await GetShellDescriptorAsync();
            var serialNumber = shellDescriptorRecord == null ? 0 : shellDescriptorRecord.SerialNumber;
            if (priorSerialNumber != serialNumber)
            {
                throw new InvalidOperationException("Invalid serial number for shell descriptor");
            }

            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Updating shell descriptor for shell '{0}'...", _shellSettings.Name);
            }

            if (shellDescriptorRecord == null)
            {
                shellDescriptorRecord = await Store.CreateAsync(enabledFeatures, parameters, CancellationToken);
            }
            else
            {
                shellDescriptorRecord = await Store.UpdateAsync(shellDescriptorRecord.Id,enabledFeatures,parameters, CancellationToken);
            }
 
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Shell descriptor updated for shell '{0}'.", _shellSettings.Name);
            }
 
            await _shellDescriptorManagerEventHandlers.InvokeAsync(e => e.Changed(shellDescriptorRecord, _shellSettings.Name), _logger);
        }
    }
}
