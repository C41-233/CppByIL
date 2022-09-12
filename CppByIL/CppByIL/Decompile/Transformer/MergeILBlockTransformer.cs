using CppByIL.Cpp.Syntax;
using CppByIL.Cpp.Syntax.IL;
using CppByIL.Cpp.Syntax.Types;

namespace CppByIL.Decompile.Transformer
{

    //删除相邻的跳转指令
    internal class MergeILBlockTransformer : ITransformer
    {
        public void Run(MethodBodyDefinition body, TransformContext context)
        {
            Walk(body);
        }

        private void Walk(SyntaxNode node)
        {
            if (node.FirstChild != null)
            {
                Walk(node.FirstChild);
            }

            SyntaxNode? current = node;
            while(current != null)
            {
                var next = current.NextSibling;
                if (current is ILGoto gt 
                    && next is ILBlock block
                    && gt.Target == block
                )
                {
                    SyntaxNode prev = gt;
                    foreach (var child in block.Children)
                    {
                        child.RemoveSelfInParent();
                        prev.Parent!.InsertChildAfter(prev, child);
                        prev = child;
                    }
                    gt.RemoveSelfInParent();
                    block.RemoveSelfInParent();
                }
                current = next;
            }
        }
    }
}
