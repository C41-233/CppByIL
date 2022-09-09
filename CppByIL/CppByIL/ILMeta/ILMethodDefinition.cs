using CppByIL.ILMeta.TypeSystem;
using System.Reflection;
using System.Reflection.Metadata;

namespace CppByIL.ILMeta
{
    public class ILMethodDefinition
    {

        private ILAssemblyDefinition assembly;
        private MethodDefinition method;

        internal ILMethodDefinition(ILAssemblyDefinition assembly, MethodDefinition method)
        {
            this.assembly = assembly;
            this.method = method;
        }

        public string Name => assembly.MetaReader.GetString(method.Name);

        private void CheckInitSignature()
        {
            if (initSignature)
            {
                return;
            }

            initSignature = true;
            var genericContext = new GenericContext();
            var signature = method.DecodeSignature(assembly.TypeProvider, genericContext);
            returnType = signature.ReturnType;

            var list = new List<Parameter>();
            foreach (var handle in method.GetParameters())
            {
                list.Add(assembly.MetaReader.GetParameter(handle));
            }

            parameters = new();
            for (int i = 0; i < signature.ParameterTypes.Length; i++)
            {
                var type = signature.ParameterTypes[i];
                parameters.Add(new ILMethodParameter(assembly, list[i], type));
            }
        }

        public ILTypeReference ReturnType
        {
            get
            {
                CheckInitSignature();
                return returnType!;
            }
        }

        public IEnumerable<ILMethodParameter> Parameters
        {
            get
            {
                CheckInitSignature();
                return parameters!;
            }
        }

        private bool initSignature;

        private ILTypeReference? returnType;

        private List<ILMethodParameter>? parameters;

        public bool IsConstructor => Name == ".ctor" && IsRTSpecialName;

        public bool IsStatic => (method.Attributes & MethodAttributes.Static) != 0;

        public bool IsRTSpecialName => (method.Attributes & MethodAttributes.RTSpecialName) != 0;

    }
}
