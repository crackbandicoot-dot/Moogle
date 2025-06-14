// Ignore Spelling: IDF

using MoogleEngine.DocumentsUtils;
using MoogleEngine.ExtensorMethods;
namespace MoogleEngine;

public class DocumentsPagesStatistics
    {
        
        
        public ISet<string> WordSet { get; }
        public ICollection<DocumentPage> SplicedDocuments{get;}
        public string[] FilesRoutes { get; }
        private  int[,] TF { get; }
        public double[] IDF { get; }
        public double[,] TF_IDF { get; }

        
        public DocumentsPagesStatistics(string dirRoute)
        {
            FilesRoutes = Directory.GetFiles(dirRoute);
            SplicedDocuments = SplitDocuments();
            WordSet = GetWordSet();
            TF= GetTF();
            IDF = GetIDF();
            TF_IDF = GetTF_IDF();
        }
        private ICollection<DocumentPage> SplitDocuments() //Returns the TF of each document
        {
            var ans = new List<DocumentPage>();
            var readerFactory = new DocumentReaderFactory();
            
            foreach (var path in this.FilesRoutes)
            {

            var reader = readerFactory.CreateReader(path);
            foreach (var page in reader.Read())
            {
                ans.Add(page);
            }
            }
            return ans;
        }
        public ISet<string> GetWordSet() // Returns the WordSet of a Universe of GoogleEngine
        {
            HashSet<string> result = new();
            foreach (var page in SplitDocuments())
            {
                foreach (string word in page.WordsFrequencies.Keys)
                {
                    result.Add(word);
                }
            }
            return result;
        }
        
        private int[,] GetTF()
        {
            int[,] ans = new int[WordSet.Count, SplicedDocuments.Count];
            int wordNumber = 0;
            foreach (var word in WordSet)
            {
                int pageNumber = 0;
                foreach (var page in SplicedDocuments)
                {
                    if (page.WordsFrequencies.TryGetValue(word, out int frequency))
                    {
                        ans[wordNumber, pageNumber] += frequency;
                    }
                    pageNumber++;
                }
                wordNumber++;
            }
            return ans;
        }
        private double[,] GetTF_IDF()
        {
            double[,] result = new double[WordSet.Count, SplicedDocuments.Count];
            for (int i = 0; i < WordSet.Count; i++)
            {
                for (int j = 0; j < SplicedDocuments.Count; j++)
                {
                    result[i, j] = IDF[i] * TF[i, j];
                }
            }
            return result;
        }
        public double[] GetIDF() 
        {
            double[] ans = new double[WordSet.Count];
       
            for (int i = 0; i < WordSet.Count; i++)
            {
                int DocumentsContainingTerm = 0;
                for (int j = 0; j < SplicedDocuments.Count; j++)
                {
                    if (TF[i, j] != 0)
                    {
                        DocumentsContainingTerm++;
                    }
                }
            if (DocumentsContainingTerm==0)
            {
                ans[i] = 0;
            }
            ans[i] = Math.Log10((float)SplicedDocuments.Count / DocumentsContainingTerm);
            }
            return ans;
        }
    }

