using CppByIL.Cpp.Syntax.Statements;
using CppByIL.Cpp.Visitor;

namespace CppByIL.Cpp.Syntax.Types
{

    public class MethodDefinition : SyntaxNode
    {

        public TypeReference ReturnType { get; init; } = TypeReference.Void;

        public TypeReference? DeclaringType { get; init; }

        public readonly IList<MethodParameter> ParameterList = new List<MethodParameter>();
        public string Name { get; }

        public BlockStatement MethodBody { get; set; } = BlockStatement.Empty;

        public MethodDefinition(string name)
        {
            Name = name;
        }

        public override void Visit(ISynctaxNodeVisitor visitor)
        {
            visitor.VisitMethodDefinition(this);
        }
    }
}
