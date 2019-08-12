using ApiSecurityTester.Dtos;
using ApiSecurityTester.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;

namespace ApiSecurityTester.Services
{
    public class TestConfigurationService : ITestConfigurationService
    {
        private readonly ILogger<TestConfigurationService> _logger;

        private const string FILENAMEHINT = "*.ApiSecurityTester.json";

        public TestConfigurationService(ILogger<TestConfigurationService> logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<SettingsFile>> LoadConfigurationsForTests(string location)
        {
            var settingFilesToRun = new List<SettingsFile>();

            try
            {
                _logger.LogInformation($"{DateTime.UtcNow} - Looking for configuration files here: {location} with the name '{FILENAMEHINT}'");

                var settingsFiles = Directory.GetFiles(location, FILENAMEHINT);

                _logger.LogInformation($"{DateTime.UtcNow} - Found {settingsFiles.Count()} files");

                foreach (var file in settingsFiles)
                    settingFilesToRun.Add(JsonConvert.DeserializeObject<SettingsFile>(await File.ReadAllTextAsync(file)));

                _logger.LogInformation($"{DateTime.UtcNow} - Finished loading settings. Loaded {settingFilesToRun.Count()} setting files.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"There was a problem loading the tests. {ex}");

                throw;
            }

            return settingFilesToRun;
        }
    }
}