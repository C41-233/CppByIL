using CppByIL.Cpp.Visitor;
using CppByIL.ILMeta.TypeSystem;

namespace CppByIL.Cpp.Syntax.Types
{
    public class TypeReference : SyntaxNode
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

        internal TypeReference(ILTypeReference type)
        {
            this.type = type;
        }

        public static TypeReference Void => Get(ILPrimitiveTypeReference.Void);

        public override void Visit(ISynctaxNodeVisitor visitor)
        {
            visitor.VisitTypeReference(this);
        }

        private static readonly Dictionary<ILTypeReference, TypeReference> pool = new();

        public static TypeReference Get(ILTypeReference type)
        {
            if (pool.TryGetValue(type, out var node))
            {
                return node;
            }

            node = new TypeReference(type);
            pool.Add(type, node);
            return node;
        }
    }
}
