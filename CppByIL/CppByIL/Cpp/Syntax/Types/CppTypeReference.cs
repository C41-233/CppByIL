using CppByIL.Cpp.Visitor;
using CppByIL.ILMeta.TypeSystem;

namespace CppByIL.Cpp.Syntax.Types
{
    public class CppTypeReference : SyntaxNode
    {

        public string FullName
        {
            get
            {
                if (type is ILPrimitiveTypeReference primitive)
                {
                    if (primitive == ILPrimitiveTypeReference.Int32)
                    {
                        return "il::int32";
                    }
                }
                return SyntaxUtils.ILFullNameToCppFullName(type.FullName);
            }
        }

        private readonly ILTypeReference type;

        internal CppTypeReference(ILTypeReference type)
        {
            this.type = type;
        }

        public static CppTypeReference Void => Get(ILPrimitiveTypeReference.Void);

        public override void Visit(Visitor.Visitor visitor)
        {
            visitor.VisitTypeReference(this);
        }

        private static readonly Dictionary<ILTypeReference, CppTypeReference> pool = new();

        public static CppTypeReference Get(ILTypeReference type)
        {
            if (pool.TryGetValue(type, out var node))
            {
                return node;
            }

            node = new CppTypeReference(type);
            pool.Add(type, node);
            return node;
        }
    }
}
