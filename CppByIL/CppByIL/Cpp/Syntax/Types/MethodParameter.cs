using CppByIL.Cpp.Visitor;

namespace CppByIL.Cpp.Syntax.Types
{
    public class MethodParameter : SyntaxNode
    {

        public string Name { get; }

        public CppTypeReference ParameterType { get; }

        public MethodParameter(string name, CppTypeReference type)
        {
            Name = name;
            ParameterType = type;
        }

        public override void Visit(Visitor.Visitor visitor)
        {
            visitor.VisitMethodParameter(this);
        }
    }
}
