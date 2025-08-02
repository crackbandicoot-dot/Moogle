using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoogleEngine.ProcessedQuery;
using MoogleEngine.TextCorpus;

namespace MoogleEngine.SearchEngine
{
    interface ISearchEngine
    {
        public PriorityQueue<SearchRank,double> Search(Query query, TextCorpus.VectorizedTextCorpus textCorpus);
  
    }

}
