using System;

namespace MathSolver.Lexing
{
    public class BracketMissingLexerException : Exception
    {
        public BracketType Type { get; set; }
        public BracketMissingLexerException(BracketType type)
        {
            Type = type;
        }
    }
}
