using CppByIL.Cpp.Syntax.Expressions;
using CppByIL.Cpp.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppByIL.Cpp.Syntax.Statements
{
    public class ExpressionStatement : Statement
    {

        public Expression Expression { get; }

        internal ExpressionStatement(Expression expression)
        {
            Expression = expression;
            AppendChild(expression);
        }

        public override void Visit(Visitor.Visitor visitor)
        {
            visitor.VisitExpressionStatement(this);
        }
    }
}
