using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoogleEngine.OperatorsCompiler
{
    internal class Token
    {
        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }

        public TokenType Type { get; }
        public string Value { get; set; }

    }
    internal enum TokenType{
        EOF,
        EXISITS,
        RELEVANT,
        NOTEXISTS,
        WORD,
        WHITESPACE
    }
}
