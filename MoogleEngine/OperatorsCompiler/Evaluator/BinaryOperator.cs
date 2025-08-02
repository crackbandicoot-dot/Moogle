// Ignore Spelling: Scorize

using MoogleEngine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoogleEngine.OperatorsCompiler.Evaluator
{
    internal abstract class BinaryOperator : Operator
    {
        private Operator _left;
        private Operator _right;
        public BinaryOperator(Operator left, Operator right)
        {
            _left = left;
            _right = right;
        }
        public override double Evaluate(SimpleDictionary<string, double> wordsFrequency)
        {
            return Scorize(_left.Evaluate(wordsFrequency),_right.Evaluate(wordsFrequency));
        }
        public abstract double Scorize(double leftScore,double rightScore);
        
    }
}
