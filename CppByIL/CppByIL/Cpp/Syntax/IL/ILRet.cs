using CppByIL.Cpp.Syntax.Expressions;

namespace CppByIL.Cpp.Syntax.IL
{
    public class ILRet : ILStatement
    {

        public RightValueExpression? Operand => (RightValueExpression?)FirstChild;

        internal ILRet()
        {
        }

        internal ILRet(ILInstruction operand)
        {
            AppendChild(operand);
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
