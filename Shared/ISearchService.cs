namespace Shared
{
    using Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the contract for the offline search engine service.
    /// This interface acts as a bridge between the UI and the underlying search logic.
    /// The actual implementation of the search algorithm is outside the scope of this UI project.
    /// </summary>
    public interface ISearchService
    {
        /// <summary>
        /// Performs a search based on a user query.
        /// </summary>
        /// <param name="query">The search term entered by the user.</param>
        /// <param name="pageNumber">The page number of the results to retrieve (1-based).</param>
        /// <param name="resultsPerPage">The number of results to return per page.</param>
        /// <returns>
        /// A Task that represents the asynchronous operation. The task result contains a list of
        /// SearchResult objects matching the query for the specified page. Returns an empty list if no results are found.
        /// </returns>
        Task<List<SearchResult>> SearchAsync(string query, int pageNumber, int resultsPerPage);

        /// <summary>
        /// Retrieves the full content of a document by its unique identifier.
        /// </summary>
        /// <param name="documentId">The unique ID of the document to retrieve.</param>
        /// <returns>
        /// A Task that represents the asynchronous operation. The task result contains the full
        /// Document object. Returns null if no document with the specified ID is found.
        /// </returns>
        Task<Document> GetDocumentByIdAsync(string documentId);
    }
}
