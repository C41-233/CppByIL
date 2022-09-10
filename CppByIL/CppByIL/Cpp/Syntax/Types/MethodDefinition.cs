using CppByIL.Cpp.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppByIL.Cpp.Syntax.Types
{

    public class MethodDefinition : SyntaxNode
    {

        public TypeReference ReturnType { get; init; } = TypeReference.Void;

        public TypeReference? DeclaringType { get; init; }

        public readonly IList<MethodParameter> ParameterList = new List<MethodParameter>();
        public string Name { get; }

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
