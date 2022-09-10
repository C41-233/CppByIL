using System.Reflection.Metadata;

namespace CppByIL.ILMeta.TypeSystem
{

    public abstract class ILTypeReference
    {

        private static readonly Dictionary<string, ILTypeReference> pool = new Dictionary<string, ILTypeReference>();

        static ILTypeReference()
        {
            AddPrimitive(PrimitiveTypeCode.Int32);
            AddPrimitive(PrimitiveTypeCode.Void);

            static void AddPrimitive(PrimitiveTypeCode code)
            {
                var type = PrimitiveTypeCodeToType(code);
                var fullname = type.FullName!;
                pool.Add(fullname, new ILPrimitiveTypeReference(fullname, PrimitiveTypeCode.Int32));

            }
        }

        public static ILTypeReference Get(string fullname)
        {
            if (pool.TryGetValue(fullname, out var r))
            {
                return r;
            }
            r = Parse(fullname);
            pool.Add(fullname, r);
            return r;
        }

        public static ILTypeReference Get(PrimitiveTypeCode code)
        {
            var type = PrimitiveTypeCodeToType(code);
            return Get(type.FullName!);
        }

        private static Type PrimitiveTypeCodeToType(PrimitiveTypeCode code)
        {
            switch (code)
            {
                case PrimitiveTypeCode.Void: return typeof(void);
                case PrimitiveTypeCode.Int32: return typeof(int);
                case PrimitiveTypeCode.Int64: return typeof(long);  
                case PrimitiveTypeCode.Double: return typeof(double);
                case PrimitiveTypeCode.Single: return typeof(float);
                case PrimitiveTypeCode.String: return typeof(string);
                default: throw new NotSupportedException();
            }
        }

        private static ILTypeReference Parse(string fullname)
        {
            var i = fullname.LastIndexOf('.');
            string? ns;
            string name;
            if (i < 0)
            {
                ns = null;
                name = fullname;
            }
            else
            {
                ns = fullname.Substring(0, i);
                name = fullname.Substring(i + 1);   
            }

            return new ILDirectTypeReference(ns, name);
        }

        public abstract string FullName { get; }


    }

}
