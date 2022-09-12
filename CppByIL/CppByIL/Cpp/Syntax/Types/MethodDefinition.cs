using CppByIL.Cpp.Syntax.Statements;
using CppByIL.Cpp.Visitor;

namespace CppByIL.Cpp.Syntax.Types
{

    public class MethodDefinition : SyntaxNode
    {

        public CppTypeReference ReturnType { get; init; } = CppTypeReference.Void;

        public CppTypeReference? DeclaringType { get; init; }

        public readonly IList<MethodParameter> ParameterList = new List<MethodParameter>();
        public string Name { get; }

        public MethodBodyDefinition MethodBody => (MethodBodyDefinition)FirstChild!;

        public MethodDefinition(string name)
        {
            Name = name;
        }

        public override void Visit(Visitor.Visitor visitor)
        {
            visitor.VisitMethodDefinition(this);
        }
    }
}
