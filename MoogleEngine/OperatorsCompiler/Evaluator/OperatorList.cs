using MoogleEngine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoogleEngine.OperatorsCompiler.Evaluator
{
    internal class OperatorList : Operator
    {
        private readonly List<Operator> operators;

        public OperatorList(List<Operator> operators)
        {
            this.operators = operators;
        }

        public override double Evaluate(SimpleDictionary<string, double> wordsFrequency)
        {
            double ans = 1;
            foreach (Operator op in operators)
            {
                var res = op.Evaluate(wordsFrequency);
                if (Math.Abs(res)<double.Epsilon&& op is RelevanceOperator)
                {
                    continue;
                }
                else if (Math.Abs(res) < double.Epsilon)
                {
                    return 0d;
                }

                ans *=res;
            }
            return ans; 
        }
    }
}
