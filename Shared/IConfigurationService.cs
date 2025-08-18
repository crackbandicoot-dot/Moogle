namespace Shared
{
    using Shared;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the contract for a service that manages application configuration.
    /// The service is responsible for persisting and retrieving SearchConfiguration settings
    /// from a JSON file.
    /// </summary>
    public interface IConfigurationService
    {
        /// <summary>
        /// Asynchronously loads the search configuration from a persistent source (e.g., a JSON file).
        /// If the configuration source does not exist, it should return a new instance with default values.
        /// </summary>
        /// <returns>A Task that represents the asynchronous operation. The task result contains the loaded SearchConfiguration.</returns>
        Task<SearchConfiguration> LoadConfigurationAsync();


        /// <summary>
        /// Asynchronously saves the provided search configuration to a persistent source (e.g., a JSON file).
        /// </summary>
        /// <param name="configuration">The SearchConfiguration object to save.</param>
        /// <returns>A Task that represents the asynchronous save operation.</returns>
        Task SaveConfigurationAsync(SearchConfiguration configuration);

    }
}
