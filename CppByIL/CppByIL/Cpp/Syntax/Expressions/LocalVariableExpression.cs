using CppByIL.Cpp.Visitor;
using CppByIL.Decompile;

namespace CppByIL.Cpp.Syntax.Expressions
{
    public class LocalVariableExpression : Expression
    {

        public string Name { get; }

        internal LocalVariableExpression(string name)
        {
            Name = name;
        }

        public override void Visit(ISynctaxNodeVisitor visitor)
        {
            visitor.VisitVariableExpression(this);
        }

    }
}
