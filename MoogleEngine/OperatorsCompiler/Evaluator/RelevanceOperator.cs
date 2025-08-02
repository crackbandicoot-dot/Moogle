using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoogleEngine.OperatorsCompiler.Evaluator
{
    internal class RelevanceOperator : UnaryOperator
    {
        public RelevanceOperator(Operator nestedOperator) : base(nestedOperator)
        {
        }

        public override double Scorize(double frequency)
        {
            return 2 * frequency;
        }
    }
}
