using CppByIL.Cpp.Syntax.Types;
using CppByIL.Cpp.Visitor;

namespace CppByIL.Cpp.Syntax.Expressions
{
    public class LocalVariableReadExpression : RightValueExpression
    {

        public string Name { get; }

        internal LocalVariableReadExpression(string name)
        {
            Name = name;
        }

        public override void Visit(Visitor.Visitor visitor)
        {
            visitor.VisitLocalVariableReadExpression(this);
        }

        public override string ToString()
        {
            return Name;
        }

    }

    public class LocalVariableWriteExpression : LeftValueExpression
    {

        public string Name { get; }
        public CppTypeReference Type { get; }

        internal LocalVariableWriteExpression(string name, CppTypeReference type)
        {
            Name = name;
            Type = type;
        }

        public override void Visit(Visitor.Visitor visitor)
        {
            visitor.VisitLocalVariableWriteExpression(this);
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
