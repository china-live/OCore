﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using XCore.Setup.Events;

namespace XCore.Settings.Services
{
    /// <summary>
    /// During setup, registers the Super User.
    /// </summary>
    public class SetupEventHandler : ISetupEventHandler
    {
        private readonly ISiteService _setupService;

        public SetupEventHandler(ISiteService setupService)
        {
            _setupService = setupService;
        }

        public async Task Setup(
            string siteName,
            string userName,
            string email,
            string password,
            string dbProvider,
            string dbConnectionString,
            string dbTablePrefix,
            Action<string, string> reportError
            )
        {
            // Updating site settings
            var siteSettings = await _setupService.GetSiteSettingsAsync();
            siteSettings.SiteName = siteName;
            siteSettings.SuperUser = userName;
            await _setupService.UpdateSiteSettingsAsync(siteSettings);

            // TODO: Add Encryption Settings in
        }
    }
}
