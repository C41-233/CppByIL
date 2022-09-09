using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CppByIL.ILMeta.TypeSystem
{

    public class ILPrimitiveTypeReference :ILTypeReference
    {

        private static readonly Dictionary<PrimitiveTypeCode, ILPrimitiveTypeReference> pool = new();

        internal static ILPrimitiveTypeReference Get(PrimitiveTypeCode code)
        {
            if (pool.TryGetValue(code, out var r))
            {
                return r;
            }

            r = new ILPrimitiveTypeReference(code);
            pool.Add(code, r);
            return r;
        }

        public static ILPrimitiveTypeReference Void => Get(PrimitiveTypeCode.Void);

        public static ILPrimitiveTypeReference Int32 => Get(PrimitiveTypeCode.Int32);

        public override string FullName { get; }

        internal ILPrimitiveTypeReference(PrimitiveTypeCode code)
        {
            if (code == PrimitiveTypeCode.Int32)
            {
                FullName = "System.Int32";
            }
            else
            {
                FullName = code.ToString();
            }
        }

    }

}
