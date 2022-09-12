
namespace CppByIL.Cpp.Syntax.IL
{
    public class ILBranch : ILStatement
    {

        public int Offset { get; }

        internal ILBranch(int offset)
        {
            Offset = offset;
        }

        public override string ToString()
        {
            return $"IL_Branch({Offset})";
        }

    }
}
