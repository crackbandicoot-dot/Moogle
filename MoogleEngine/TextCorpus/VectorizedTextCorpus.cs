// Ignore Spelling: IDF
// Ignore Spelling: IDF

using MoogleEngine.DocumentsUtils;
using MoogleEngine.Text;
using MoogleEngine.TextReader;
using MoogleEngine.Utils;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.Marshalling;
using System.Text.Json;
namespace MoogleEngine.TextCorpus;

public class VectorizedTextCorpus
{
    public int Cardinality { get; }
    public TextPage[] Pages { get; }
    public VectorizedText[] VectorizedPages { get; }
    

    public VectorizedTextCorpus(string dirRoute)
    {
        TextCorpus textCorpus = TextCorpusCache.DeserializeOrCreate(dirRoute); ;
        
    
        List<TextPage> pages = new();
        foreach (var document in textCorpus.Documents.Values)
        {
            foreach (var page in document.Pages)
            {
                pages.Add(page);
            }
        }
        Pages = pages.ToArray();
        
        Cardinality = Pages.Length;
        var idf = GetIDF(Pages);
        VectorizedPages = GetVectorizedPages(Pages,idf);
        
    }
    
    private VectorizedText[] GetVectorizedPages(TextPage[] texts,
        SimpleDictionary<string,double> idf)
    {
        
        var ans = new VectorizedText[Cardinality];
        int t = 0;
        foreach (var text in texts)
        {
            var tf_idf = new SimpleDictionary<string, double>();
            foreach (var word in text.WordsFrequencies.Keys)
            {
                tf_idf[word] = TF(Cardinality,text.WordsFrequencies[word]) * idf[word];
            }
            ans[t] = new VectorizedText(tf_idf);
            t++;
        }
        return ans;
    }

    private SimpleDictionary<string, double> GetIDF(TextPage[] pages)
    {
        var ans = new SimpleDictionary<string, double>();

        foreach (var page in pages)
        {
            foreach (var word in page.WordsFrequencies.Keys)
            {
                ans[word]++;
            }
        }
        foreach (var word in ans.Keys)
        {
            ans[word] = IDF(Cardinality, ans[word]);
        }
        return ans;

    }

    private double TF(int numberOfDocuments, double termFrequency)
    {
        return termFrequency;
    }
    private double IDF(int numberOfDocuments,double termFrequency)
    {
        return Math.Log((numberOfDocuments - termFrequency + 0.5d) / (termFrequency + 0.5d) + 1);
    }
}

