using CppByIL.Cpp.Syntax.Expressions;
using CppByIL.Cpp.Visitor;

namespace CppByIL.Cpp.Syntax.IL
{

    public abstract class ILInstruction : RightValueExpression
    {

        public sealed override void Visit(Visitor.Visitor visitor)
        {
            visitor.VisitILInstruction(this);
        }

    }

}
