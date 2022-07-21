namespace MathSolver.Parsing
{
    public class StaticExpression : IExpression
    {
        public decimal Value { get; set; }
        public decimal Evaluate()
        {
            return Value;
        }
    }
}
