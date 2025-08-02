using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoogleEngine.OperatorsCompiler.Evaluator
{
    internal class ExistsOperator : UnaryOperator
    {
        public ExistsOperator(Operator nestedOperator) : base(nestedOperator)
        {
        }

        public override double Scorize(double frequency)
        {
            return Math.Sign(frequency);
        }
    }
}
