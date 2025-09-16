// Ignore Spelling: IDF

using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using MoogleEngine.AppConfig;
using MoogleEngine.DocumentsUtils;
using MoogleEngine.SearchEngine;
using MoogleEngine.TextReader;
using MoogleEngine.Utils;
using UglyToad.PdfPig.Content;


namespace MoogleEngine;


public class Moogle :ISearchService
{
    private IList<SearchItem> _lastQueryResult;

    private string _lastQuery;
    private TextCorpus.VectorizedTextCorpus textCorpus;

    private AppConfig.AppConfig settings;

    private static ITextReaderFactory readerFactory;
    
    public Moogle()
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appconfig.json")
            .Build();
        IConfigurationService configurationService = new ConfigurationService();
        settings = configurationService.LoadConfigurationAsync().Result;
        string path = Path.GetFullPath(settings.DataBasePath);
        textCorpus = new TextCorpus.VectorizedTextCorpus(path);
        readerFactory = new TextReaderFactory();
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

    public Task<IList<SearchItem>> Search(string query)
    {
        IList<SearchItem> searchResult = new List<SearchItem>();
        if (_lastQuery == query)
        {
            searchResult = _lastQueryResult;
        }
        else
        {
            _lastQuery = query;
            _lastQueryResult = searchResult = Query(query).Items().ToList();
        }
        
        return Task.FromResult(searchResult);
    }
}

