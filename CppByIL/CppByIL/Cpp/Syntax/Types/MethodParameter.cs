using CppByIL.Cpp.Visitor;

namespace CppByIL.Cpp.Syntax.Types
{
    public class MethodParameter : SyntaxNode
    {

        public string Name { get; }

        public TypeReference ParameterType { get; }

        public MethodParameter(string name, TypeReference type)
        {
            Name = name;
            ParameterType = type;
        }

        public override void Visit(ISynctaxNodeVisitor visitor)
        {
            visitor.VisitMethodParameter(this);
        }
    }
}
