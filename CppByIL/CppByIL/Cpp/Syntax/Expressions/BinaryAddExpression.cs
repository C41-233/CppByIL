using CppByIL.Cpp.Visitor;

namespace CppByIL.Cpp.Syntax.Expressions
{
    public class BinaryAddExpression : RightValueBinaryExpression
    {

        public BinaryAddExpression(RightValueExpression left, RightValueExpression right) 
            : base(BinaryOperator.Add, left, right)
        {
        }

        public override void Visit(Visitor.Visitor visitor)
        {
            visitor.VisitBinaryExpression(this);
        }
    }
}
