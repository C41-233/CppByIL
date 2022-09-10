using CppByIL.Cpp.Visitor;

namespace CppByIL.Cpp.Syntax.Statements
{
    public class MethodBody : SyntaxNode
    {
        public static readonly MethodBody Empty = new MethodBody();

        public override void Visit(ISynctaxNodeVisitor visitor)
        {
            visitor.VisitMethodBody(this);
        }
    }
}
