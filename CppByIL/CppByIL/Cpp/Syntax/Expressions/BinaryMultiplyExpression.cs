using CppByIL.Cpp.Visitor;

namespace CppByIL.Cpp.Syntax.Expressions
{
    public class BinaryMultiplyExpression : RightValueBinaryExpression
    {

        public BinaryMultiplyExpression(RightValueExpression left, RightValueExpression right)
            : base(BinaryOperator.Multiply, left, right)
        {
        }

        public override void Visit(Visitor.Visitor visitor)
        {
            visitor.VisitBinaryExpression(this);
        }
    }
}
