using CppByIL.Cpp.Syntax;
using CppByIL.Cpp.Syntax.IL;
using CppByIL.Cpp.Syntax.Types;

namespace CppByIL.Decompile.Transformer
{

    //合并变量的读写，后一个读与前一个写合并
    internal class MergeILVariableTransformer : ITransformer
    {

        public void Run(MethodBodyDefinition body, TransformContext context)
        {
            var sites = new Dictionary<string, ILLocalStore>();
            foreach(var statement in body.Statements)
            {
                Walk(statement, sites);
            }
        }

        private void Walk(SyntaxNode node, Dictionary<string, ILLocalStore> sites)
        {
            foreach (var child in node.Children)
            {
                Walk(child, sites);
            }

            if (node is ILLocalStore st)
            {
                sites[st.Variable.Name] = st;
            }
            else if (node is ILLocalLoad ld)
            {
                if (sites.TryGetValue(ld.Variable.Name, out var site))
                {
                    site.RemoveSelfInParent();
                    var expr = site.Expression;
                    expr.RemoveSelfInParent();
                    ld.ReplaceSelfInParent(expr);
                }
            }
        }
    }
}
