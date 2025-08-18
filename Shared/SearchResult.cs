namespace Shared
{
    /// <summary>
    /// Represents a single item in the list of search results.
    /// This model is used to display a brief overview of a document that matches a search query.
    /// </summary>
    public class SearchResult
    {
        /// <summary>
        /// A unique identifier for the document this search result points to.
        /// This ID will be used to retrieve the full document content.
        /// </summary>
        public string DocumentId { get; set; } = string.Empty;

        /// <summary>
        /// The title of the document.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// A short text snippet from the document that is relevant to the search query.
        /// The length of this snippet can be configured in SearchConfiguration.
        /// </summary>
        public string Snippet { get; set; } = string.Empty;
    }
}
