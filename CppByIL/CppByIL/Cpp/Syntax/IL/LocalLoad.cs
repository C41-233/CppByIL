using CppByIL.Decompile;

namespace CppByIL.Cpp.Syntax.IL
{

    public class LocalLoad : ILInstruction
    {

        public ILVariable Variable { get; }

        internal LocalLoad(ILVariable variable)
        {
            Variable = variable;
        }

        public override string ToString()
        {
            return $"IL_Load({Variable.Name})";
        }
    }

}
