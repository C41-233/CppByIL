using CppByIL.Cpp.Syntax.Statements;
using CppByIL.Cpp.Visitor;

namespace CppByIL.Cpp.Syntax.IL
{
    public class ILStatement : Statement
    {

        public ILStatement()
        {
        }

        public override void Visit(Visitor.Visitor visitor)
        {
            visitor.VisitILStatement(this);
        }

    }
}
