using MoogleEngine.Utils;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoogleEngine.OperatorsCompiler.Evaluator
{
    internal class Word : Operator
    {
        private string _word;

        public Word(string word)
        {
            _word = word;
        }

        public override double Evaluate(SimpleDictionary<string, double> wordsFrequency)
        {
            return wordsFrequency[_word];
        }
    }
}
