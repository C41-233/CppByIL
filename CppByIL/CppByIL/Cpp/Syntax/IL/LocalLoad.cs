using CppByIL.Decompile;

namespace CppByIL.Cpp.Syntax.IL
{

    public class LocalLoad : ILInstruction
    {

        private ILVariable variable;

        internal LocalLoad(ILVariable variable)
        {
            this.variable = variable;
        }

        public override string ToString()
        {
            return $"IL_Load({variable.Name})";
        }
    }

}
