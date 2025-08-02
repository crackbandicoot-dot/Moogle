using MoogleEngine.OperatorsCompiler.Evaluator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoogleEngine.OperatorsCompiler
{
    internal static class OperatorCompiler
    {
       public static Operator Compile(string queryString)
        {
            Lexer.Lexer lexer = new(queryString);
            Parser.Parser parser = new Parser.Parser(lexer.Tokenize());
            return parser.Parse();
        }
    }
}
