using System.Collections.Immutable;
using System.Reflection.Metadata;

namespace CppByIL.ILMeta.TypeSystem;

internal class TypeProvider : ISignatureTypeProvider<ILTypeReference, GenericContext>
{

    private Dictionary<string, ILTypeReference> pool = new Dictionary<string, ILTypeReference>();

    public ILTypeReference GetArrayType(ILTypeReference elementType, ArrayShape shape)
    {
        throw new NotImplementedException();
    }

    public ILTypeReference GetByReferenceType(ILTypeReference elementType)
    {
        throw new NotImplementedException();
    }

    public ILTypeReference GetFunctionPointerType(MethodSignature<ILTypeReference> signature)
    {
        throw new NotImplementedException();
    }

    public ILTypeReference GetGenericInstantiation(ILTypeReference genericType, ImmutableArray<ILTypeReference> typeArguments)
    {
        throw new NotImplementedException();
    }

    public ILTypeReference GetGenericMethodParameter(GenericContext genericContext, int index)
    {
        throw new NotImplementedException();
    }

    public ILTypeReference GetGenericTypeParameter(GenericContext genericContext, int index)
    {
        throw new NotImplementedException();
    }

    public ILTypeReference GetModifiedType(ILTypeReference modifier, ILTypeReference unmodifiedType, bool isRequired)
    {
        throw new NotImplementedException();
    }

    public ILTypeReference GetPinnedType(ILTypeReference elementType)
    {
        throw new NotImplementedException();
    }

    public ILTypeReference GetPointerType(ILTypeReference elementType)
    {
        throw new NotImplementedException();
    }

    public ILTypeReference GetPrimitiveType(PrimitiveTypeCode typeCode)
    {
        if (typeCode == PrimitiveTypeCode.Int32)
        {
            return ILPrimitiveTypeReference.Get(typeCode);
        }
        throw new NotImplementedException();
    }

    public ILTypeReference GetSZArrayType(ILTypeReference elementType)
    {
        throw new NotImplementedException();
    }

    public ILTypeReference GetTypeFromDefinition(MetadataReader reader, TypeDefinitionHandle handle, byte rawTypeKind)
    {
        throw new NotImplementedException();
    }

    public ILTypeReference GetTypeFromReference(MetadataReader reader, TypeReferenceHandle handle, byte rawTypeKind)
    {
        throw new NotImplementedException();
    }

    public ILTypeReference GetTypeFromSpecification(MetadataReader reader, GenericContext genericContext, TypeSpecificationHandle handle, byte rawTypeKind)
    {
        throw new NotImplementedException();
    }

}
