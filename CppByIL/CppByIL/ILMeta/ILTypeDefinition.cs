using CppByIL.Utils.Collections;
using System.Reflection.Metadata;

namespace CppByIL.ILMeta
{

    public class ILTypeDefinition
    {

        private ILAssemblyDefinition assembly;
        private TypeDefinition definition;

        internal ILTypeDefinition(ILAssemblyDefinition assembly, TypeDefinitionHandle handle)
        {
            this.assembly = assembly;
            definition = assembly.MetaReader.GetTypeDefinition(handle);
        }

        public bool IsTopLevelType => definition.GetDeclaringType().IsNil;

        public string Name => assembly.MetaReader.GetString(definition.Name);

        public string? Namespace
        {
            get
            {
                if (definition.Namespace.IsNil)
                {
                    return null;
                }
                return assembly.MetaReader.GetString(definition.Namespace);
            }
        }

        public string FullName
        {
            get
            {
                var ns = Namespace;
                if (ns == null)
                {
                    return Name;
                }
                return $"{ns}.{Name}";
            }
        }

        private IndexDictionary<MethodDefinitionHandle, ILMethodDefinition>? methods;

        public IEnumerable<ILMethodDefinition> Methods
        {
            get
            {
                CheckInitMethods();
                foreach (var method in methods!.Values)
                {
                    yield return method;
                }
            }
        }

        private void CheckInitMethods()
        {
            if (methods != null)
            {
                return;
            }
            methods = new IndexDictionary<MethodDefinitionHandle, ILMethodDefinition>();
            foreach (var handle in definition.GetMethods())
            {
                var method = assembly.MetaReader.GetMethodDefinition(handle);
                methods.Add(handle, new ILMethodDefinition(assembly, method));
            }
        }

        public override string ToString()
        {
            return Name;
        }

    }

}
