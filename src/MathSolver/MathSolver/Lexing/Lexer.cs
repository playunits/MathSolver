using System;
using System.Collections.Generic;

namespace MathSolver.Lexing
{

    public class Lexer
    {

        public (int, string) GetNumber(int start, string search)
        {
            string tmp = string.Empty;

            do
            {
                tmp += search[start];
                start++;
            } while (start < search.Length && "0123456789,".Contains(search[start]));

            return (start - 1, tmp);
        }

        public List<Token> GetTokens(string input)
        {
            List<Token> tokens = new List<Token>();
            int index = 0;
            string tmpString = string.Empty;
            while (index < input.Length)
            {
                char currentCharacter = input[index];

                if ("(".Contains(currentCharacter))
                {
                    tokens.Add(new BracketToken()
                    {
                        Type = TokenType.Bracket,
                        BracketType = BracketType.Opening
                    });

                }
                else if (")".Contains(currentCharacter))
                {
                    tokens.Add(new BracketToken()
                    {
                        Type = TokenType.Bracket,
                        BracketType = BracketType.Closing
                    });
                }
                else if ("+-*/".Contains(currentCharacter))
                {
                    tokens.Add(new CommandToken()
                    {
                        Type = TokenType.Command,
                        Operation = (MathOperation)(int)currentCharacter
                    });
                }
                else if ("0123456789,".Contains(currentCharacter))
                {
                    (int tmpIndex, string number) result = GetNumber(index, input);

                    string numberString = result.number;
                    index = result.tmpIndex;

                    if (!string.IsNullOrWhiteSpace(numberString))
                    {
                        if (Decimal.TryParse(numberString.Replace(',', '.'), out decimal number))
                        {
                            tokens.Add(new NumberToken()
                            {
                                Type = TokenType.Number,
                                Number = number
                            });
                        }


                    }

                }
                else
                {
                    throw new UnknownTokenLexerException();
                }
                index++;
            }

            return tokens;
        }

        public void ValidateTokens(List<Token> tokens)
        {
            // Rules:
            //      No Command next to command
            //      Brackets have to close

            Stack<BracketToken> bracketStorage = new Stack<BracketToken>();

            for (int i = 0; i < tokens.Count; i++)
            {
                Token token = tokens[i];

                if (token.Type is TokenType.Bracket)
                {
                    BracketToken bt = (token as BracketToken)!;

                    if (bt.BracketType is BracketType.Opening)
                    {
                        bracketStorage.Push(bt);
                    }
                    else if (bt.BracketType is BracketType.Closing)
                    {
                        if (!bracketStorage.TryPop(out BracketToken _))
                        {
                            throw new BracketMissingLexerException(BracketType.Opening);
                        }
                    }
                }
                else if (token.Type is TokenType.Command)
                {
                    int tmp = i + 1;
                    if (tmp < tokens.Count)
                    {
                        Token nextToken = tokens[tmp];
                        if (nextToken.Type is TokenType.Command)
                        {
                            throw new AdjacentCommandsLexerException();
                        }
                    }
                }
            }

            if (bracketStorage.Count > 0)
            {
                throw new BracketMissingLexerException(BracketType.Closing);
            }
        }
    }
}
