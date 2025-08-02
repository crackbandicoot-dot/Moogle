// Ignore Spelling: Lexer

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MoogleEngine.OperatorsCompiler.Lexer
{

    internal class Lexer
    {
        
        private int _currentPos;
        
        private readonly string _input;
        
        public Lexer(string input)
        {
            _input = input;
        }
        private HashSet<char> _querySymbols=new() { 
        '\0','^','!','*'
        };
        private void AdvanceChar()
        {
            _currentPos++;
        }
        private char CurrentChar()
        {
            char c = _currentPos >= _input.Length ? '\0':_input[_currentPos];
            return c ;
        }
        private Token CurrentToken()
        {
            char c = CurrentChar();
            switch (c)
            {
                case '^':
                    AdvanceChar();
                    return new Token(TokenType.EXISITS, "^");
                case '!':
                    AdvanceChar();
                    return new Token(TokenType.NOTEXISTS, "!");
                case '*':
                    AdvanceChar();
                    return new Token(TokenType.RELEVANT, "*");
                case '\0':
                    return new Token(TokenType.EOF, "EOF");
                
                default:
                    if (char.IsWhiteSpace(c))
                    {
                        while (char.IsWhiteSpace(CurrentChar()))
                        {
                            AdvanceChar();
                        }
                        return new Token(TokenType.WHITESPACE, "");
                    }
                    else
                    {
                        string word = $"{c}";
                        AdvanceChar();
                        while (!_querySymbols.Contains(CurrentChar())&&
                            !char.IsWhiteSpace(CurrentChar()))
                        { 
                            word += CurrentChar();
                            AdvanceChar();
                        }
                        return new Token(TokenType.WORD, word);
                    }
            }
        }
        
        public IList<Token> Tokenize()
        {
            //Empieza con el primer token
            //en el penultimo token pide 

            var ans = new List<Token>();
            var currentToken = CurrentToken();
            ans.Add(currentToken);
            while (currentToken.Type!=TokenType.EOF)
            {
               currentToken =CurrentToken();
                if (currentToken.Type!=TokenType.WHITESPACE)
                {
                    ans.Add(currentToken);
                }
            }
            return ans;

        }

    }
}
