using CppByIL.ILMeta.TypeSystem;
using System.Reflection.Metadata;

namespace CppByIL.ILMeta
{
    public class ILMethodParameter
    {

        private readonly ILAssemblyDefinition assembly;
        public ILTypeReference ParameterType { get; }

        private Parameter parameter;

        public string Name => assembly.MetaReader.GetString(parameter.Name);

        internal ILMethodParameter(ILAssemblyDefinition assembly, Parameter parameter, ILTypeReference type)
        {
            this.assembly = assembly;
            ParameterType = type;
            this.parameter = parameter;
        }

    }
}
