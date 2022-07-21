using System;

namespace MathSolver.Parsing
{
    public class DynamicExpression : IExpression
    {
        public IExpression Left { get; set; }
        public IExpression Right { get; set; }
        public MathOperation operation { get; set; }

        public decimal Evaluate()
        {
            decimal left = Left.Evaluate();
            decimal right = Right.Evaluate();

            decimal result = operation switch
            {
                MathOperation.Add => left + right,
                MathOperation.Subtract => left - right,
                MathOperation.Divide => left / right,
                MathOperation.Multiply => left * right,
                _ => throw new Exception("")
            };

            return result;
        }
    }
}
