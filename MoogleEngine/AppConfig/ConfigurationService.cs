using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MoogleEngine.AppConfig
{
    public class ConfigurationService : IConfigurationService
    {
        private static string _configPath= @"C:\Users\PC\Desktop\QuickAcces\Moogle\MoogleEngine\appconfig.json";
        public Task<AppConfig> LoadConfigurationAsync()
        {
            
            var serializedConfig = File.ReadAllText(_configPath);
            var config = JsonSerializer.Deserialize<AppConfig>(serializedConfig);
            return Task.FromResult(config);
        }

        public Task SaveConfigurationAsync(AppConfig configuration)
        {
            var serializedConfig = JsonSerializer.Serialize(configuration);
            File.WriteAllText(_configPath, serializedConfig);
            return Task.CompletedTask;
        }
    }
}
