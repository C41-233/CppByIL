using CppByIL.ILMeta.TypeSystem;
using System.Reflection;
using System.Reflection.Metadata;

namespace CppByIL.ILMeta
{
    public class ILMethodDefinition
    {

        internal ILAssemblyDefinition Assembly;
        internal MethodDefinition Method;

        internal ILMethodDefinition(ILAssemblyDefinition assembly, ILTypeDefinition type, MethodDefinition method)
        {
            this.Assembly = assembly;
            this.Method = method;
            DeclaringType = type;
        }

        public ILTypeDefinition DeclaringType { get; }

        public string Name => Assembly.MetaReader.GetString(Method.Name);

        private void CheckInitSignature()
        {
            if (initSignature)
            {
                return;
            }

            initSignature = true;
            var genericContext = new GenericContext();
            var signature = Method.DecodeSignature(TypeProvider.Instance, genericContext);
            returnType = signature.ReturnType;

            var list = new List<Parameter>();
            foreach (var handle in Method.GetParameters())
            {
                list.Add(Assembly.MetaReader.GetParameter(handle));
            }

            parameters = new();
            for (int i = 0; i < signature.ParameterTypes.Length; i++)
            {
                var type = signature.ParameterTypes[i];
                parameters.Add(new ILMethodParameter(Assembly, list[i], type));
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

        public IReadOnlyList<ILMethodParameter> Parameters
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

        public bool IsStatic => (Method.Attributes & MethodAttributes.Static) != 0;

        public bool IsRTSpecialName => (Method.Attributes & MethodAttributes.RTSpecialName) != 0;

        public MethodBodyBlock GetMethodBody()
        {
            var body = Assembly.PEReader.GetMethodBody(Method.RelativeVirtualAddress);
            return body;
        }
    }
}
