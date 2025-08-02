using MoogleEngine.OperatorsCompiler;
using MoogleEngine.OperatorsCompiler.Evaluator;
using MoogleEngine.TextCorpus;
using MoogleEngine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoogleEngine.ProcessedQuery
{
    internal class Query
    {
        private Operator op;
        public Query(string queryString)
        {
            QueryString = queryString;
            op = OperatorCompiler.Compile(queryString);
        }

        public string QueryString { get; }
        public  double ScoreFunction(SimpleDictionary<string,double> wordsFrequency)
        {
            return op.Evaluate(wordsFrequency);
        }


    }
}
