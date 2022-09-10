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
        public readonly IList<MethodParameter> ParameterList = new List<MethodParameter>();

        public override void Visit(ISynctaxNodeVisitor visitor)
        {
            visitor.VisitMethodDeclaration(this);
        }
    }
}
