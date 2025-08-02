using MoogleEngine.DocumentsUtils;
using MoogleEngine.ProcessedQuery;
using MoogleEngine.Text;
using MoogleEngine.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoogleEngine.SearchEngine
{
    class SearchEngine : ISearchEngine
    {
        public PriorityQueue<SearchRank,double> Search(Query query,
            TextCorpus.VectorizedTextCorpus textCorpus)
        {
            var results = new (SearchRank,double)[textCorpus.Cardinality];
            VectorizedText queryVector = new(query.QueryString.CountWords());
            for (int p = 0; p < textCorpus.Cardinality; p++)
            {
                double score =
                    queryVector.Cos(
                    textCorpus.VectorizedPages[p]
                    );
                score *= query.ScoreFunction(textCorpus.VectorizedPages[p].Base);
                results[p] = (new SearchRank(textCorpus.Pages[p],score),-score);
            }
           
            return new(results);
            
            
        }
    }
}
