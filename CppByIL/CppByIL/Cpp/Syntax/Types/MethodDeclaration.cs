using CppByIL.Cpp.Visitor;

namespace CppByIL.Cpp.Syntax.Types
{
    public class MethodDeclaration : SyntaxNode
    {

        public string Name { get; }

        public MethodDeclaration(string name)
        {
            Name = name;
        }

        public bool IsStatic { get; init; }

        public TypeReference ReturnType { get; init; } = TypeReference.Void;
        public readonly IList<MethodParameterDeclaration> ParameterDeclarations = new List<MethodParameterDeclaration>();

        public override void Visit(ISynctaxNodeVisitor visitor)
        {
            visitor.VisitMethodDeclaration(this);
        }
    }
}
