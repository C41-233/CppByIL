using CppByIL.Cpp.Syntax.Precessor;
using CppByIL.Cpp.Visitor;

namespace CppByIL.Cpp.Syntax
{

    public class SyntaxTree : SyntaxNode
    {

        public override string ToString()
        {
            var writer = new StringWriter();
            var visitor = new CppWriterVisitor(writer);
            foreach (var child in Children)
            {
                child.Visit(visitor);
            }
            return writer.ToString();
        }

        public override void Visit(Visitor.Visitor visitor)
        {
        }

    }

}
