using CppByIL.Cpp.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppByIL.Cpp.Syntax.Expressions
{
    public class AssignmentExpression : Expression
    {

        public Expression LeftValue { get; }

        public Expression RightValue { get; }

        internal AssignmentExpression(Expression left, Expression right)
        {
            LeftValue = left;
            RightValue = right;
        }

        public override void Visit(ISynctaxNodeVisitor visitor)
        {
            visitor.VisitAssignmentExpression(this);
        }
    }
}
