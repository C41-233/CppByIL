using CppByIL.Cpp.Visitor;

namespace CppByIL.Cpp.Syntax.Precessor
{
    public class Pragma : SyntaxNode
    {

        public string Value { get; }

        public Pragma(string value)
        {
            Value = value;
        }

        public override void Visit(Visitor.Visitor visitor)
        {
            visitor.VisitPragmaPrecessor(this);
        }
    }
}
