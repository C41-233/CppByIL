namespace CppByIL.Cpp.Syntax.Expressions
{

    public enum BinaryOperator
    {
        Add,
        Subtract,
        Multiply,
    }

    public abstract class RightValueBinaryExpression : RightValueExpression
    {
        public BinaryOperator Operator { get; }

        public RightValueExpression Left => (RightValueExpression)FirstChild!;

        public RightValueExpression Right => (RightValueExpression)LastChild!;

        protected RightValueBinaryExpression(BinaryOperator op, RightValueExpression left, RightValueExpression right)
        {
            Operator = op;
            AppendChild(left);
            AppendChild(right);
        }

        public override string ToString()
        {
            return $"{Left} {Operator} {Right}";
        }

    }

}
