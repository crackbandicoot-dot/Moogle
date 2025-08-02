using MoogleEngine.OperatorsCompiler.Lexer;
using MoogleEngine.OperatorsCompiler.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoogleEngine.Utils
{
    public static class TestTools
    {
        public static void TestParser()
        {
            //Score esperado 12 
            SimpleDictionary<string,double> d1 = new SimpleDictionary<string, double>
            {
                //{ "baby", 2 },
                { "la", 1 },
                { "es", 3 }
            };
            //Score esperado 1
            SimpleDictionary<string, double> d2 = new SimpleDictionary<string, double>
            {
                //{ "baby", 2 },
                { "la", 1 },
                { "ciclo", 3 }
            };
            //Score esperado 0
            SimpleDictionary<string, double> d3 = new SimpleDictionary<string, double>
            {
               // { "baby", 2 },
                //{ "la", 1 },
                { "ciclo", 3 }
            };
            string input = "!baby   ^la vida **es un ciclo   ";
            Lexer l = new Lexer(input);
            Parser p = new Parser(l.Tokenize());
            var r = p.Parse();
            var answer1 = r.Evaluate(d1);
            var answer2 = r.Evaluate(d2);
            var answer3 = r.Evaluate(d3);
        }
    }
}
