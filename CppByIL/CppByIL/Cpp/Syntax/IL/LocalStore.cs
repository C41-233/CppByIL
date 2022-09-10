using CppByIL.Decompile;

namespace CppByIL.Cpp.Syntax.IL
{
    public class LocalStore : ILInstruction
    {

        public ILVariable Variable { get; }
        public ILInstruction Instruction { get; }

        internal LocalStore(ILVariable varibale, ILInstruction inst)
        {
            Variable = varibale;
            Instruction = inst;
        }

        public override string ToString()
        {
            return $"IL_Store({Variable.Name}, {Instruction})";
        }

    }
}
