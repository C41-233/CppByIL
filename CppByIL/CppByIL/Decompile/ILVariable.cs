using CppByIL.ILMeta.TypeSystem;

namespace CppByIL.Decompile
{
    internal class ILVariable
    {

        public ILTypeReference Type { get; }
        public string Name { get; }

        public ILVariable(ILTypeReference type, string name)
        {
            Type = type;
            Name = name;
        }
    }

}
