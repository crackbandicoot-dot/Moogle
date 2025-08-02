using MoogleEngine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoogleEngine.OperatorsCompiler.Evaluator
{
    internal abstract class Operator
    {
        public abstract double Evaluate(SimpleDictionary<string,double> wordsFrequency);
    }
}
