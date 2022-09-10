using System.Reflection.Metadata;

namespace CppByIL.ILMeta.TypeSystem
{

    public class ILPrimitiveTypeReference :ILTypeReference
    {

        public static ILTypeReference Void => Get(PrimitiveTypeCode.Void);

        public static ILTypeReference Int32 => Get(PrimitiveTypeCode.Int32);

        public override string FullName { get; }

        internal ILPrimitiveTypeReference(string fullname, PrimitiveTypeCode code)
        {
            FullName = fullname;
        }

    }

}
