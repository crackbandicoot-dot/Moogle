// Ignore Spelling: IDF

using System.Text.RegularExpressions;
namespace MoogleEngine;

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

