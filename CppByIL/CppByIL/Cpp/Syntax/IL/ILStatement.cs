using CppByIL.Cpp.Syntax.Statements;
using CppByIL.Cpp.Visitor;

namespace CppByIL.Cpp.Syntax.IL
{
    public class ILStatement : Statement
    {

        public ILInstruction Instruction { get; }

        public ILStatement(ILInstruction instruction)
        {
            Instruction = instruction;
        }

        public override void Visit(ISynctaxNodeVisitor visitor)
        {
            visitor.VisitILStatement(this);
        }

    }
}
