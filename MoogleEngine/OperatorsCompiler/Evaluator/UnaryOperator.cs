// Ignore Spelling: Scorize

using MoogleEngine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoogleEngine.OperatorsCompiler.Evaluator
{
    internal abstract class UnaryOperator : Operator
    {
        private Operator _nestedOperator;

        protected UnaryOperator(Operator nestedOperator)
        {
            _nestedOperator = nestedOperator;
        }

        public override double Evaluate(SimpleDictionary<string, double> wordsFrequency)
        {
            return Scorize(_nestedOperator.Evaluate(wordsFrequency));
        }
        public abstract double Scorize(double frequency);
        
    }
}
