using CppByIL.Cpp.Visitor;

namespace CppByIL.Cpp.Syntax.Precessor
{
    public class Include : SyntaxNode
    {

        public string FileName { get; }

        public Include(string filename)
        {
            FileName = filename;
        }

        public override void Visit(Visitor.Visitor visitor)
        {
            visitor.VisitIncludePrecessor(this);
        }
    }
}
