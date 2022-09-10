namespace CppByIL.Cpp.Syntax.IL
{
    public class BinaryNumericInstruction : ILInstruction
    {

        public BinaryNumericOperator Operator { get; }

        public ILInstruction Left { get; }

        public ILInstruction Right { get; } 

        internal BinaryNumericInstruction(BinaryNumericOperator op, ILInstruction left, ILInstruction right)
        {
            Operator = op;
            Left = left;
            Right = right;
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
    }
}
