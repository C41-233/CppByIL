using CppByIL.Cpp.Syntax;
using CppByIL.Cpp.Syntax.IL;
using CppByIL.Cpp.Syntax.Statements;

namespace CppByIL.Decompile.Transformer
{

    internal class RemoveNopTransformer : ITransformer
    {
        public void Run(SyntaxNode body, TransformContext context)
        {
            var toRemoves = new List<SyntaxNode>();
            foreach (var node in body.Children)
            {
                if (node is Nop)
                {
                    toRemoves.Add(node);
                }
            }
            foreach (var node in toRemoves)
            {
                node.RemoveSelfInParent();
            }
        }
    }

}
