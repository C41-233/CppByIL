using CppByIL.Cpp.Visitor;

namespace CppByIL.Cpp.Syntax.Types
{
    public class ClassDeclaration : SyntaxNode
    {

        public string Name { get; }

        public ClassDeclaration(string name)
        {
            Name = name;
        }

        public override void Visit(Visitor.Visitor visitor)
        {
            visitor.VisitClassDeclaration(this);
        }

    }
}
