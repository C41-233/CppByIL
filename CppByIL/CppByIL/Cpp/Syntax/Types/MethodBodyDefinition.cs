
using CppByIL.Cpp.Syntax.Statements;
using CppByIL.Cpp.Visitor;

namespace CppByIL.Cpp.Syntax.Types
{

    public class MethodBodyDefinition : SyntaxNode
    {

        public static readonly MethodBodyDefinition Empty = new MethodBodyDefinition();


        public IEnumerable<Statement> Statements => Children.Cast<Statement>();

        public override void Visit(Visitor.Visitor visitor)
        {
            visitor.VisitMethodBodyDefinition(this);
        }
    }

}
