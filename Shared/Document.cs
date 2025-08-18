namespace Shared
{
    /// <summary>
    /// Represents a full document with its complete content.
    /// This model is used when a user clicks on a search result to view the entire document.
    /// </summary>
    public class Document
    {
        /// <summary>
        /// The unique identifier for the document. This should match the DocumentId in the corresponding SearchResult.
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// The full title of the document.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// The complete content of the document.
        /// </summary>
        public string Content { get; set; } = string.Empty;
    }
}
