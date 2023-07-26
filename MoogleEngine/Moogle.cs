using System.Text.RegularExpressions;
using System.IO;
namespace MoogleEngine;


public static class Moogle
{
  
    private static Documents documents;

    public static void initDocs()
    {
        string ruta = Path.GetFullPath("../Content");
        documents = new Documents(ruta);
    }
    public static SearchResult Query(string query)
    {
        
        Query userInp = new Query(query, documents.WordSet, documents.IDF);
        double[] scores = GetScores(userInp);
        double[] positiveScores = scores.Where(x => x > 0).ToArray();
        int PosScoresLength = positiveScores.Length;
        string[] results = GetResults(scores);
 
        Array.Sort(scores, results);
        Array.Reverse(results);
        Array.Reverse(scores);
        SearchItem[] items = new SearchItem[PosScoresLength];
        for (int i = 0; i < PosScoresLength; i++)
        {
            items[i] = new SearchItem(Path.GetFileName(results[i]), ExtractText(results[i],query) ,(float)scores[i]);
        }
        return new SearchResult(items, query);

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
        for (int i = 0; i < documents.TF_IDF.GetLength(1); i++)
        {
            results.Add(documents.DocTF.ElementAt(i).Key);
        }
        return results.ToArray();
    }
    public static string ExtractText(string filePath, string query)
    {
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
public class Documents
    {
        //Instance Variables
        private string DirRoute;
        private string[] filesroutes;
        private Dictionary<string, Dictionary<string, int>> docTF;
        private string[] wordset;
        private int[,] tf;
        private double[] idf;
        private double[,] tf_idf;
    //Properties
        public string[] FilesRoutes
        {
            get
            {
                return this.filesroutes;
            }
        }
        public Dictionary<string, Dictionary<string, int>> DocTF //TF of each document
        {
            get
            {
                return docTF;
            }
        }
        public string[] WordSet //Word Set Propertie
        {
            get
            {

                return wordset;
            }
        }
        private int[,] TF
        {
            get
            {
                return tf;
            }
        } //Term Absolute Frequency
          //Public Properties
        public double[] IDF
        {
            get
            {
                return idf;
            }
        }
        public double[,] TF_IDF
        {
            get
            {
                return tf_idf;
            }
        }
        //Methods
        public Documents(string DirRoute) //Constructor
        {
            this.DirRoute = DirRoute;
            this.filesroutes = Directory.GetFiles(DirRoute, "*.txt");
            this.docTF = ComputeDocTF();
            this.wordset = GetWordSet();
            this.tf = GetTF();
            this.idf = GetIDF();
            this.tf_idf = GetTF_IDF();
        }
        private Dictionary<string, Dictionary<string, int>> ComputeDocTF() //Returns the TF of each document
        {
            Dictionary<string, Dictionary<string, int>> result = new Dictionary<string, Dictionary<string, int>>();
            foreach (var FileRoute in this.FilesRoutes)
            {
                Dictionary<string, int> Doc = new Dictionary<string, int>();
                foreach (var word in TokenizeDocument(FileRoute))
                {
                    if (!Doc.ContainsKey(word))
                    {
                        Doc[word.ToLower()] = 1;
                    }
                    else
                    {
                        Doc[word]++;
                    }
                }
            result.Add(FileRoute, Doc);
            //    result.Add(Path.GetFileName(FileRoute), Doc);
            }
            return result;
        }
        public string[] GetWordSet() // Returns the WordSet of a Universe of Documents
        {
            HashSet<string> result = new HashSet<string>();
            foreach (KeyValuePair<string, Dictionary<string, int>> Doc in this.DocTF)
            {
                foreach (string word in Doc.Value.Keys)
                {
                    result.Add(word);

                }
            }
            return result.ToArray();
        }
        private string[] TokenizeDocument(string DocRoute) // Tokenize a Document
        {
            string CurrentDocument = File.ReadAllText(DocRoute);
            string[] words = Regex.Split(CurrentDocument, @"\W+").Where(elem => elem != " " && elem != "").ToArray();
            return words;
        }
        private int[,] GetTF()
        {
            int[,] result = new int[this.WordSet.Length, DocTF.Count];
            for (int wordIndex = 0; wordIndex < this.WordSet.Length; wordIndex++)
            {
                int counter = 0;
                foreach (Dictionary<string, int> keyValuePairs in this.DocTF.Values)
                {
                    if (keyValuePairs.ContainsKey(this.WordSet[wordIndex]))
                    {
                        result[wordIndex, counter] = keyValuePairs[this.WordSet[wordIndex]];
                    }
                    counter++;
                }
            }
            return result;
        }
        private double[,] GetTF_IDF()
        {
            double[,] result = new double[this.TF.GetLength(0), this.TF.GetLength(1)];
            for (int i = 0; i < this.TF.GetLength(0); i++)
            {
                double scalar = this.IDF[i];
                for (int j = 0; j < this.TF.GetLength(1); j++)
                {
                    result[i, j] = scalar * TF[i, j];
                }
            }
            return result;
        }
        public double[] GetIDF() //Returns the Documents IDF
        {
            double[] idf = new double[this.TF.GetLength(0)];
            int numberOfDocuments = this.TF.GetLength(1);
            //  Console.WriteLine("Numero de Documentos: "+numberOfDocuments);
            for (int i = 0; i < this.TF.GetLength(0); i++)
            {
                int DocumentsContainingTerm = 0;
                for (int j = 0; j < this.TF.GetLength(1); j++)
                {
                    if (this.TF[i, j] != 0)
                    {
                        DocumentsContainingTerm++;
                    }
                }
                // Console.WriteLine("Numero de documentos que contienen al termino");
                idf[i] = Math.Log10((float)numberOfDocuments / DocumentsContainingTerm);
            }
            return idf;
        }
    }
    public class Query
    {

        //Instance Variables
        private string query;
        private string[] WordSet;
        private double[] IDF;
        private string[] tokenizedquery;
        private int[] tf;
        private double[] tf_idf;

        //Properties
        private string[] TokenizedQuery
        {
            get
            {
                return this.tokenizedquery;
            }
        }
        private int[] TF
        {
            get
            {
                return this.tf;
            }
        }
        public double[] TF_IDF
        {
            get
            {
                return tf_idf;
            }
        }



        //Methods 
        public Query(string query, string[] WordSet, double[] IDF) //Constructor
        {
            this.query = query.ToLower();
            this.WordSet = WordSet;
            this.IDF = IDF;
            this.tokenizedquery = GetTokenizedQuery();
            this.tf = GetTF();
            this.tf_idf = GetTF_IDF();
        }
        private string[] GetTokenizedQuery()
        {
            return Regex.Split(this.query, @"\W+").Where(elem => elem != " " && elem != "").ToArray();
        }
        public int[] GetTF()
        {
            int[] result = new int[WordSet.Length];
            for (int i = 0; i < WordSet.Length; i++)
            {
                if (this.TokenizedQuery.Contains(WordSet[i].ToLower()))
                {
                    result[i] = this.TokenizedQuery.Count(word => word == WordSet[i]);
                }
            }
            return result;
        }
        public double[] GetTF_IDF()
        {
            double[] result = new double[this.TF.Length];
            for (int i = 0; i < this.TF.Length; i++)
            {
                result[i] = this.TF[i] * this.IDF[i];
            }
            return result;
        }
    }
    public class Vector
    {
        //Intstance Variables
        private double[] components;
        private int dimension;
        private double module;
        //Properties
        private double Module
        {
            get { return module; }
        }
        //Methods
        public Vector(double[] components) //Constructor
        {
            this.components = components;
            this.dimension = this.components.Length;
            this.module = GetModule();
        }
        private double GetModule()
        {
            double Sum = 0;
            foreach (var component in this.components)
            {
                Sum += (float)Math.Pow(component, 2);
            }
            return Math.Sqrt(Sum);
        }
        private static double DotProduct(Vector v1, Vector v2)
        {
            double res = 0;
            for (int i = 0; i < v1.dimension; i++)
            {
                res += v1.components[i] * v2.components[i];
            }
            return res;
        }
        public static double Cos(Vector v1, Vector v2)
        {
            if (v1.Module != 0 && v2.Module != 0)
            {
                return DotProduct(v1, v2) / (v1.Module * v2.Module);
            }
            else
            {
                return 0;
            }
        }

    }

