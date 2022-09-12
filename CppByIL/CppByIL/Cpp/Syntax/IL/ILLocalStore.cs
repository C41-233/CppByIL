using CppByIL.Cpp.Syntax.Expressions;
using CppByIL.Decompile;

namespace CppByIL.Cpp.Syntax.IL
{
    public class ILLocalStore : ILStatement
    {

        public ILVariable Variable { get; }
        public RightValueExpression Expression => (RightValueExpression)FirstChild!;

        internal ILLocalStore(ILVariable varibale, ILInstruction inst)
        {
            Variable = varibale;
            AppendChild(inst);
        }

        public override string ToString()
        {
            return $"IL_Store({Variable.Name}, {Expression})";
        }

    }
}
