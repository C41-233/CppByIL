using CppByIL.Decompile;

namespace CppByIL.Cpp.Syntax.IL
{

    public class ILLocalLoad : ILInstruction
    {

        public ILVariable Variable { get; }

        internal ILLocalLoad(ILVariable variable)
        {
            Variable = variable;
        }

        public override string ToString()
        {
            return $"IL_Load({Variable.Name})";
        }
    }

}
