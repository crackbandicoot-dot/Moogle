// Ignore Spelling: IDF

using System.Text.RegularExpressions;
using System.IO;
using Microsoft.Extensions.Configuration;
using MoogleEngine.DocumentsUtils;
using MoogleEngine.ExtensorMethods;
namespace MoogleEngine;


public static class Moogle
{
  
    private static DocumentsPagesStatistics documents;

    public static AppConfig Settings;
    public static void initDocs()
    {
        
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appconfig.json")
            .Build();

        // Bind configuration to class
        Settings = config.GetSection("AppConfig").Get<AppConfig>() ?? throw new Exception("Configuration missing!");
        string path = Path.GetFullPath(Settings.DataBasePath);
        documents = new DocumentsPagesStatistics(path);
    }

    public static SearchResult Query(string query)
    {
        
        Query userInp = new Query(query, documents.WordSet.ToArray(), documents.IDF);
        double[] scores = GetScores(userInp);
        double[] validScores = scores.Where(x => x > Settings.MinimumScoreLength).ToArray();
        string[] results = GetResults(scores);
        int[] pages = GetPages(scores);
        Array.Sort(scores, results);
        Array.Sort(scores, pages);
        Array.Reverse(results);
        Array.Reverse(scores);
        Array.Reverse(pages);
        int numberOfItemsToDisplay = Math.Min(Settings.NumbersOfResultsShowed, validScores.Length);
        SearchItem[] items = new SearchItem[numberOfItemsToDisplay];
        for (int i = 0; i < numberOfItemsToDisplay; i++)
        {
            items[i] = new SearchItem(Path.GetFileName(results[i]), ExtractText(results[i],query), (float)scores[i], pages[i]);
        }
        return new SearchResult(items, query);

    }
    public static int[] GetPages(double[] scores)
    {
        List<int> pages = new();
        foreach (var page in documents.SplicedDocuments)
        {
            pages.Add(page.PageNumber);
        }
        return pages.ToArray();
    }
    public static double[] GetScores(Query userInp)
    {
        double[] result = new double[documents.TF_IDF.GetLength(1)];
        Vector queryVector = new Vector(userInp.TF_IDF);
        for (int i = 0; i < documents.TF_IDF.GetLength(1); i++)
        {
            double[] doc = new double[documents.TF_IDF.GetLength(0)];
            for (int j = 0; j < documents.TF_IDF.GetLength(0); j++)
            {
                doc[j] = documents.TF_IDF[j, i];
            }
            Vector docVecor = new Vector(doc);
            result[i] = Vector.Cos(queryVector, docVecor);
        }
        return result;
    }
    public static string[] GetResults(double[] scores)
    {
        List<string> results = new List<string>();
        foreach (var page in documents.SplicedDocuments)
        {
            results.Add(page.OriginalDocumentPath);
        }
        return results.ToArray();
    }
    public static string ExtractText(string filePath, string query)
    {
        if (!filePath.EndsWith("txt"))
        {
            return "...[Too large Document]...";
        }
        string text = File.ReadAllText(filePath);
        string[] words = query.Split(' ');
        int start = -1;
        int end = -1;
        foreach (string word in words)
        {
            string pattern = $"(?i){Regex.Escape(word)}";
            Match match = Regex.Match(text, pattern);
            if (match.Success)
            {
                int index = match.Index;
                if (start == -1 || index < start)
                {
                    start = Math.Max(0, index - 50);
                }
                if (end == -1 || index + match.Length > end)
                {
                    end = Math.Min(text.Length - 1, index + match.Length + 50);
                }
            }
        }
        if (start == -1 || end == -1)
        {
            return "No se encontró ninguna coincidencia.";
        }
        return text.Substring(start, end - start);
    }
}

