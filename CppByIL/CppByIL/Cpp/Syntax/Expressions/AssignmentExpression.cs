using CppByIL.Cpp.Visitor;

namespace CppByIL.Cpp.Syntax.Expressions
{
    public class AssignmentExpression : RightValueExpression
    {

        public LeftValueExpression LeftValue => (LeftValueExpression)FirstChild!;

        public RightValueExpression RightValue => (RightValueExpression)LastChild!;

        internal AssignmentExpression(LeftValueExpression left, RightValueExpression right)
        {
            AppendChild(left);
            AppendChild(right);
        }

        public override void Visit(Visitor.Visitor visitor)
        {
            visitor.VisitAssignmentExpression(this);
        }
    }
}
