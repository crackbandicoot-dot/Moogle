using MoogleEngine.OperatorsCompiler.Evaluator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig.DocumentLayoutAnalysis.WordExtractor;

namespace MoogleEngine.OperatorsCompiler.Parser
{
    internal class Parser
    {
        private readonly IList<Token> tokens;
        private int _tokenPos;
        
        public Parser(IList<Token> tokens) 
        {
            this.tokens = tokens;
        }
        public Token CurrentToken()
        {
            return tokens[_tokenPos];
        }
        public void Consume()
        {
            _tokenPos++;
        }
        public bool Match(TokenType type)
        {
            return CurrentToken().Type==type;
        }
        
        public Operator Parse()
        {
            var operators = new List<Operator>();
            while (CurrentToken().Type!=TokenType.EOF)
            {
                var unary = Unary();
                if (unary != null && unary is not Evaluator.Word)
                {
                    operators.Add(unary);
                }
            }
            return new OperatorList(operators);
        }
        public Operator? Unary()
        {
            if (Match(TokenType.WORD))
            {
                return Word();
            }
            else if (Match(TokenType.EXISITS))
            {
                Consume();
                if (Match(TokenType.WORD))
                {
                    var word = Word();
                    if (word != null) { 
                        return new ExistsOperator(word);
                    }
                    return default(Operator);
                }
                return default(Operator);
            }
            else if (Match(TokenType.NOTEXISTS))
            {
                Consume();
                if (Match(TokenType.WORD))
                {
                    var word = Word();
                    if (word != null)
                    {
                        return new NotExistsOperator(word);
                    }
                    return default(Operator);
                    
                }
                return default(Operator);
            }
            else if(Match(TokenType.RELEVANT))
            {
                Consume();
                var unary = Unary();
                if (unary is null ||
                    (unary is not Evaluator.Word && unary is not RelevanceOperator))
                {
                    return default(Operator);
                }
                return new RelevanceOperator(unary);

            }
            else
            {
                return default(Operator);
            }

        }
        public Operator? Word()
        {
            if (Match(TokenType.WORD))
            {
                var ans = new Word(CurrentToken().Value);
                Consume();
                return ans;
            }
            return default(Operator);
        }

    }
    
}
