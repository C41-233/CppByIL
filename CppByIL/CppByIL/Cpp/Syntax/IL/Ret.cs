namespace CppByIL.Cpp.Syntax.IL
{
    public class Ret : ILInstruction
    {

        public ILInstruction? Operand { get; }

        internal Ret()
        {
        }

        internal Ret(ILInstruction operand)
        {
            Operand = operand;
        }

        public override string ToString()
        {
            if (Operand == null)
            {
                return $"IL_Ret";
            }
            else
            {
                return $"IL_Ret({Operand})";
            }
        }
    }
}
