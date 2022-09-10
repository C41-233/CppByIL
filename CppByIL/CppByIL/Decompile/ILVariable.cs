using CppByIL.ILMeta.TypeSystem;

namespace CppByIL.Decompile
{
    public class ILVariable
    {

        public ILVariableKind Kind { get; }
        public ILTypeReference Type { get; }
        public string Name { get; }

        public ILVariable(ILVariableKind kind, ILTypeReference type, string name)
        {
            Kind = kind;
            Type = type;
            Name = name;
        }
    }

    public enum ILVariableKind
    {
        Parameter,
        Local,
        StackSlot,
    }

}
