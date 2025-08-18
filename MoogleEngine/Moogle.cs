// Ignore Spelling: IDF

using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using MoogleEngine.DocumentsUtils;
using MoogleEngine.SearchEngine;
using MoogleEngine.TextReader;
using MoogleEngine.Utils;
using UglyToad.PdfPig.Content;
using Shared;

namespace MoogleEngine;


public class Moogle :ISearchService
{

    private TextCorpus.VectorizedTextCorpus textCorpus;

    private AppConfig.AppConfig settings;

    private static ITextReaderFactory readerFactory;
    
    public Moogle()
    {
        //IConfiguration config = new ConfigurationBuilder()
        //    .AddJsonFile("appconfig.json")
        //    .Build();

        //// Bind configuration to class
        //settings = config.GetSection("AppConfig").Get<AppConfig.AppConfig>() ?? throw new Exception("Configuration missing!");
        //string path = Path.GetFullPath(settings.DataBasePath);
        //textCorpus = new TextCorpus.VectorizedTextCorpus(path);
        //readerFactory = new TextReaderFactory();
    }
    

    public SearchResult Query(string query)
    {

        ISearchEngine searchEngine = new SearchEngine.SearchEngine();
        var results = searchEngine.Search(new MoogleEngine.ProcessedQuery.Query(query),textCorpus);
        
        SimpleDictionary<string, List<int>> textPages = new();
        SimpleDictionary<string, string> textSnippets = new();

        int pagesToShow = Math.Min(results.Count, settings.NumbersOfResultsShowed);
        for (int p = 0; p < pagesToShow; p++)
        {
            var x = results.Dequeue();
            var page = x.Page; 
            var score = x.Score;
            
            if (textPages[page.PathToDocument] == null)
            {
                    textPages[page.PathToDocument] = [];

                    textSnippets[page.PathToDocument] =
                        ExtractText(page.PathToDocument, query, page.PageNumber);

            }

            textPages[page.PathToDocument]
               .Add(page.PageNumber);
            
        }      
        SearchItem[] items = new SearchItem[textPages.Count];
        int itemsIndex = 0;
        foreach (var title in textPages.Keys)
        {
            items[itemsIndex] = new SearchItem(title, textSnippets[title].ToString(), textPages[title]);
            itemsIndex++;
            
        }
        return new SearchResult(items);
    }
    
    
    
    public static string ExtractText(string filePath, string query,int pageNumber)
    {
        
        var reader = readerFactory.CreateReader(filePath);
        
        string text = reader.ReadPage(pageNumber);
        
        string[] words = query.Split(' ');
        int start = -1;
        int end = -1;
        int snippetLength=50;
        foreach (string word in words)
        {
            string pattern = $"(?i){Regex.Escape(word)}";
            Match match = Regex.Match(text, pattern);
            if (match.Success)
            {
                int index = match.Index;
                if (start == -1 || index < start)
                {
                    start = Math.Max(0, index - snippetLength/2);
                }
                if (end == -1 || index + match.Length > end)
                {
                    end = Math.Min(text.Length - 1, index + match.Length + snippetLength/2);
                }
            }
        }
        if (start == -1 || end == -1)
        {
            return "...";
        }
        return text[start..end];
    }

    public Task<List<Shared.SearchResult>> SearchAsync(string query, int pageNumber, int resultsPerPage)
    {
        return Task.FromResult(new List<Shared.SearchResult>
        {
            new Shared.SearchResult
            {
                DocumentId = "1",
                Title = "Sample Document",
                Snippet = "This is a sample snippet from the document."
            }
        });
    }

    public Task<Shared.Document> GetDocumentByIdAsync(string documentId)
    {
        // Basic test implementation: returns a dummy document
        return Task.FromResult(new Shared.Document
        {
            Id = documentId,
            Title = "Test Document",
            Content = "This is the full content of the test document."
        });
    }
}

