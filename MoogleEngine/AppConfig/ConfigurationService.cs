using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace MoogleEngine.AppConfig
{
    public class ConfigurationService : IConfigurationService
    {
        public Task<SearchConfiguration> LoadConfigurationAsync()
        {
            // Return a default SearchConfiguration for testing purposes
            var config = new SearchConfiguration
            {
                ResultsPerPage = 2,
                SnippetLength = 400,
                Theme = "Dark",
                SearchIndexPath = "test-index.json"
            };
            return Task.FromResult(config);
        }

        public Task SaveConfigurationAsync(SearchConfiguration configuration)
        {
            // Simulate saving by doing nothing (testing stub)
            return Task.CompletedTask;
        }
    }
}
