using CppByIL.Cpp.Syntax.Expressions;
using CppByIL.Cpp.Syntax.Types;
using CppByIL.Cpp.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppByIL.Cpp.Syntax.Statements
{
    public class LocalVariableDeclareStatement : Statement
    {

        public string Name { get; }

        public CppTypeReference Type { get; }

        public RightValueExpression? Expression => (RightValueExpression?)FirstChild;

        internal LocalVariableDeclareStatement(string name, CppTypeReference type, RightValueExpression expression)
        {
            Name = name;
            Type = type;
            AppendChild(expression);
        }

        public override void Visit(Visitor.Visitor visitor)
        {
            visitor.VisitLocalVariableDeclareStatement(this);
        }
    }
}
