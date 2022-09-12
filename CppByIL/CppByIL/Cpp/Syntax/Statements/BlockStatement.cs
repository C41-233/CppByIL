using CppByIL.Cpp.Visitor;

namespace CppByIL.Cpp.Syntax.Statements
{

    public abstract class BlockStatement : Statement
    {

        public IEnumerable<Statement> Statements => Children.OfType<Statement>();

    }

}
