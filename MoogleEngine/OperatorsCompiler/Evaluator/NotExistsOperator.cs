using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoogleEngine.OperatorsCompiler.Evaluator
{
    internal class NotExistsOperator : UnaryOperator
    {
        public NotExistsOperator(Operator nestedOperator) : base(nestedOperator)
        {
        }

        public override double Scorize(double frequency)
        {
            return Math.Abs(1-Math.Sign(frequency));
        }
    }
}
