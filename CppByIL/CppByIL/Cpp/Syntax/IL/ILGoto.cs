namespace CppByIL.Cpp.Syntax.IL
{
    public class ILGoto : ILStatement
    {

        public ILBlock Target { get; }

        internal ILGoto(ILBlock target)
        {
            Target = target;
        }

        public override string ToString()
        {
            return $"IL_Goto({Target.Name})";
        }

    }
}
