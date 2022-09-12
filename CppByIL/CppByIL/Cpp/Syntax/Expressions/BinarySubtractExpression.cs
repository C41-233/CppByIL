using CppByIL.Cpp.Visitor;

namespace CppByIL.Cpp.Syntax.Expressions
{
    public class BinarySubtractExpression : RightValueBinaryExpression
    {

        public BinarySubtractExpression(RightValueExpression left, RightValueExpression right)
            : base(BinaryOperator.Subtract, left, right)
        {
        }

        public override void Visit(Visitor.Visitor visitor)
        {
            visitor.VisitBinaryExpression(this);
        }
    }
}
