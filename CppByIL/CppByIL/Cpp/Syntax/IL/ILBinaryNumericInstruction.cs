using CppByIL.Cpp.Syntax.Expressions;

namespace CppByIL.Cpp.Syntax.IL
{
    public class ILBinaryNumericInstruction : ILInstruction
    {

        public BinaryNumericOperator Operator { get; }

        public RightValueExpression Left => (RightValueExpression)FirstChild!;

        public RightValueExpression Right => (RightValueExpression)LastChild!;

        internal ILBinaryNumericInstruction(BinaryNumericOperator op, ILInstruction left, ILInstruction right)
        {
            Operator = op;
            AppendChild(left);
            AppendChild(right);
        }

        public override string ToString()
        {
            return $"IL_{Operator}({Left}, {Right})";
        }

    }

    public enum BinaryNumericOperator
    {
        Add,
        Sub,
        Mul,
    }
}
