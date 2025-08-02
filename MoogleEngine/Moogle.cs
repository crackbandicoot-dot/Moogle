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
namespace MoogleEngine;


public static class Moogle
{
  
    private static TextCorpus.VectorizedTextCorpus textCorpus;

    public static AppConfig.AppConfig Settings;

    private static ITextReaderFactory readerFactory;
    public static void initDocs()
    {
        
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appconfig.json")
            .Build();

        // Bind configuration to class
        Settings = config.GetSection("AppConfig").Get<AppConfig.AppConfig>() ?? throw new Exception("Configuration missing!");
        string path = Path.GetFullPath(Settings.DataBasePath);
        textCorpus = new TextCorpus.VectorizedTextCorpus(path);
        readerFactory = new TextReaderFactory();
    }

    public static SearchResult Query(string query)
    {

        ISearchEngine searchEngine = new SearchEngine.SearchEngine();
        var results = searchEngine.Search(new MoogleEngine.ProcessedQuery.Query(query),textCorpus);
        
        SimpleDictionary<string, List<int>> textPages = new();
        SimpleDictionary<string, string> textSnippets = new();

        int pagesToShow = Math.Min(results.Count, Settings.NumbersOfResultsShowed);
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
}

