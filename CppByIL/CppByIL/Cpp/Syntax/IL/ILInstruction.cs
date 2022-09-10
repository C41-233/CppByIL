using CppByIL.Cpp.Visitor;

namespace CppByIL.Cpp.Syntax.IL
{

    public abstract class ILInstruction : SyntaxNode
    {

        public sealed override void Visit(ISynctaxNodeVisitor visitor)
        {
            visitor.VisitILInstruction(this);
        }

    }

}
