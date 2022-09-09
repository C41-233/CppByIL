using CppByIL.Cpp.Visitor;
using CppByIL.ILMeta.TypeSystem;

namespace CppByIL.Cpp.Syntax.Types
{
    public class MethodParameterDeclaration : SyntaxNode
    {

        public string Name { get; }

        public TypeReference ParameterType { get; }

        public MethodParameterDeclaration(string name, TypeReference type)
        {
            Name = name;
            ParameterType = type;
        }

        public override void Visit(ISynctaxNodeVisitor visitor)
        {
            visitor.VisitMethodParameterDeclaration(this);
        }
    }
}
