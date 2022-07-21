using MathSolver.Lexing;

Lexer lexer = new Lexer();

List<Token> tokens = lexer.GetTokens("5+5*5");

lexer.ValidateTokens(tokens);

Console.ReadLine();