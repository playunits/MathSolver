using MathSolver.Lexing;
using System.Collections.Generic;
using System.Linq;

namespace MathSolver.Parsing
{
    public class ParserToken
    {
        public Token Token { get; set; }
        public bool Used { get; set; } = false;
    }

    public class Parser
    {
        public IExpression Parse(List<Token> tokens)
        {
            // 1. Alle Tokens zwischen Klammern raussuchen und durch "Parse" jagen
            // 2. Multiply und Divide von Links nach rechts auflösen
            // 3. alle anderen von links nach rechts

            // 5+5*5
            // Dynamic Expr
            // L: S5 O:A R:
            //          Dynamic Expr
            //          L: S5 O:M R: S5

            // Patterns:
            //      Zahl + Command
            //      Command + ( Bracket | Zahl )
            //      Bracket + Zahl

            DynamicExpression? rootExpression = null;
            DynamicExpression? lastExpression = null;

            for (int i = 0; i < tokens.Count; i++)
            {
                Token token = tokens[i];
                Token? next = i + i >= tokens.Count ? null : tokens[i + 1];

                if (token.Type is TokenType.Number)
                {
                    NumberToken valueToken = (token as NumberToken)!;
                    
                    if(rootExpression is null)
                    {
                        rootExpression = new DynamicExpression();
                        lastExpression = rootExpression;
                    }

                    lastExpression.Left = new StaticExpression()
                    {
                        Value = valueToken.Number
                    };

                    if(next is not null)
                    {
                        CommandToken commandToken = (next as CommandToken)!;

                        if(commandToken.Operation is MathOperation.Multiply or MathOperation.Divide)
                        {
                            DynamicExpression newExpression = new DynamicExpression();
                            lastExpression.Right = newExpression;
                            lastExpression = newExpression;
                        }
                        else
                        {

                        }
                    }
                }
            }            

        }
    }
}
