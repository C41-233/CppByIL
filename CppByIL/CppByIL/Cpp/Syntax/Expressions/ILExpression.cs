using CppByIL.Cpp.Syntax.IL;
using CppByIL.Cpp.Visitor;

namespace CppByIL.Cpp.Syntax.Expressions
{
    public class ILExpression : Expression
    {

        private readonly ILInstruction inst;

        internal ILExpression(ILInstruction inst)
        {
            this.inst = inst;
        }

        public override void Visit(ISynctaxNodeVisitor visitor)
        {
            inst.Visit(visitor);
        }

    }

}
