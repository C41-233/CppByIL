using CppByIL.Cpp.Syntax.Expressions;
using CppByIL.Cpp.Visitor;

namespace CppByIL.Cpp.Syntax.Statements
{
    public class ReturnStatement : Statement
    {

        public RightValueExpression? Expression => (RightValueExpression?)FirstChild;

        internal ReturnStatement()
        {
        }

        internal ReturnStatement(RightValueExpression expression)
        {
            AppendChild(expression);
        }

        public override void Visit(Visitor.Visitor visitor)
        {
            visitor.VisitReturnStatement(this);
        }

    }
}
