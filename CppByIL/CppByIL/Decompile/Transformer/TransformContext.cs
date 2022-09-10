using CppByIL.Cpp.Syntax.Types;

namespace CppByIL.Decompile.Transformer
{
    internal class TransformContext
    {

        private readonly Dictionary<string, TypeReference> locals = new();

        public TypeReference? GetLocalVariable(string name)
        {
            return locals.GetValueOrDefault(name);
        }

        public void SetLocalVariable(string name, TypeReference type)
        {
            locals.Add(name, type);
        }
    }
}
