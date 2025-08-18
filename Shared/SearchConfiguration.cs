namespace Shared
{
    /// <summary>
    /// Represents the user-configurable settings for the search engine UI.
    //  This class is designed to be serialized to and deserialized from a JSON file.
    /// </summary>
    public class SearchConfiguration
    {
        /// <summary>
        /// Gets or sets the maximum number of search results to display per page.
        /// </summary>
        public int ResultsPerPage { get; set; } = 10;

        /// <summary>
        /// Gets or sets the maximum length of the text snippet to display for each search result.
        /// </summary>
        public int SnippetLength { get; set; } = 150;

        /// <summary>
        /// Gets or sets the theme of the application, e.g., "Light" or "Dark".
        /// </summary>
        public string Theme { get; set; } = "Light";

        /// <summary>
        /// Gets or sets the file path to the offline search index data.
        /// The UI needs this to pass to the underlying search logic.
        /// </summary>
        public string SearchIndexPath { get; set; } = string.Empty;
    }
}
