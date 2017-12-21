using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace XCore.Environment.Extensions.Manifests
{
    //组合模式Composite

    public class CompositeManifestProvider: IManifestProvider
    {
        private readonly IManifestProvider[] _manifestProviders;

        public CompositeManifestProvider(params IManifestProvider[] manifestProviders)
        {
            _manifestProviders = manifestProviders ?? new IManifestProvider[0];
        }

        public CompositeManifestProvider(IEnumerable<IManifestProvider> manifestProviders)
        {
            if (manifestProviders == null)
            {
                throw new ArgumentNullException(nameof(manifestProviders));
            }
            _manifestProviders = manifestProviders.ToArray();
        }

        public int Order { get { throw new NotSupportedException(); } }

        public IConfigurationBuilder GetManifestConfiguration(
            IConfigurationBuilder configurationBuilder,
            string filePath)
        {
            foreach (var provider in _manifestProviders)
            {
                configurationBuilder = provider.GetManifestConfiguration(
                    configurationBuilder,
                    filePath
                    );
            }

            return configurationBuilder;
        }
    }
}
