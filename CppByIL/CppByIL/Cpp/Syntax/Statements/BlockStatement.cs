using CppByIL.Cpp.Visitor;

namespace CppByIL.Cpp.Syntax.Statements
{

    public class BlockStatement : Statement
    {
        public static readonly BlockStatement Empty = new BlockStatement();

        public override void Visit(ISynctaxNodeVisitor visitor)
        {
            visitor.VisitBlockStatement(this);
        }

    }

}
