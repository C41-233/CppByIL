using CppByIL.Cpp.Syntax;
using CppByIL.Cpp.Syntax.Expressions;
using CppByIL.Cpp.Syntax.IL;
using CppByIL.Cpp.Syntax.Statements;
using CppByIL.Cpp.Syntax.Types;

namespace CppByIL.Decompile.Transformer;

//将IL变量的读写转为C++变量的读写
//转化后不再存在任何LocalStore和LocalLoad指令
internal class ILVariableTransformer : ITransformer
{

    public void Run(MethodBodyDefinition body, TransformContext context)
    {
        foreach (var statement in body.Statements)
        {
            TryReplace(statement);
        }
    }

    private void TryReplace(SyntaxNode node)
    {
        foreach(var child in node.Children)
        {
            TryReplace(child);
        }
        if (node is ILLocalStore st)
        {
            var left = new LocalVariableWriteExpression(
                st.Variable.Name,
                CppTypeReference.Get(st.Variable.Type)
            );
            TryReplace(st.Expression);
            var right = st.Expression;
            right.RemoveSelfInParent();
            var expr = new AssignmentExpression(left, right);
            node.ReplaceSelfInParent(new ExpressionStatement(expr));
        }
        else if (node is ILLocalLoad ld)
        {
            var expr = new LocalVariableReadExpression(ld.Variable.Name);
            node.ReplaceSelfInParent(expr);
        }
    }

}
