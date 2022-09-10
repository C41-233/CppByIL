using CppByIL.ILMeta.TypeSystem;
using CppByIL.Utils.Collections;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;

namespace CppByIL.ILMeta
{

    public class ILAssemblyDefinition
    {

        public static ILAssemblyDefinition Load(string path)
        {
            PEReader reader;
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                reader = new PEReader(fs, PEStreamOptions.PrefetchEntireImage);
            }
            return new ILAssemblyDefinition(reader);
        }

        internal PEReader PEReader { get; }
        
        internal MetadataReader MetaReader { get; }

        private ILAssemblyDefinition(PEReader reader)
        {
            PEReader = reader;
            MetaReader = reader.GetMetadataReader();
            foreach (var handle in MetaReader.TypeDefinitions)
            {
                var definition = new ILTypeDefinition(this, handle);
                types.Add(handle, definition);
            }
        }

        private readonly IndexDictionary<TypeDefinitionHandle, ILTypeDefinition> types = new IndexDictionary<TypeDefinitionHandle, ILTypeDefinition>();

        public IEnumerable<ILTypeDefinition> TopLevelTypes
        {
            get
            {
                foreach (var type in types.Values)
                {
                    if (!type.IsTopLevelType)
                    {
                        continue;
                    }
                    yield return type;
                }
            }
        }

        public ILTypeDefinition? GetTypeDefinition(TypeDefinitionHandle handle)
        {
            return types.GetValueOrDefault(handle);
        }

    }

}
