using CppByIL.Cpp.Syntax;
using CppByIL.Cpp.Syntax.Expressions;
using CppByIL.Cpp.Syntax.IL;
using CppByIL.Cpp.Syntax.Statements;
using CppByIL.Cpp.Syntax.Types;

namespace CppByIL.Decompile.Transformer
{

    /// <summary>
    /// IL指令转Cpp语句
    /// 转化后不应存在任何IL指令
    /// </summary>
    internal class ILOperatorTransform : ITransformer
    {
        public void Run(MethodBodyDefinition body, TransformContext context)
        {
            foreach(var statement in body.Statements)
            {
                TryReplace(statement);
            }
        }

        private void TryReplace(SyntaxNode node)
        {
            foreach (var child in node.Children)
            {
                TryReplace(child);
            }

            if (node is ILNop)
            {
                node.RemoveSelfInParent();
                return;
            }

            if (node is ILBinaryNumericInstruction il)
            {
                if (il.Operator == BinaryNumericOperator.Add)
                {
                    var left = il.Left;
                    var right = il.Right;
                    left.RemoveSelfInParent();
                    right.RemoveSelfInParent();
                    var expr = new BinaryAddExpression(left, right);
                    node.ReplaceSelfInParent(expr);
                }
                else if (il.Operator == BinaryNumericOperator.Sub)
                {
                    var left = il.Left;
                    var right = il.Right;
                    left.RemoveSelfInParent();
                    right.RemoveSelfInParent();
                    var expr = new BinarySubtractExpression(left, right);
                    node.ReplaceSelfInParent(expr);
                }
                else if (il.Operator == BinaryNumericOperator.Mul)
                {
                    var left = il.Left;
                    var right = il.Right;
                    left.RemoveSelfInParent();
                    right.RemoveSelfInParent();
                    var expr = new BinaryMultiplyExpression(left, right);
                    node.ReplaceSelfInParent(expr);
                }
                return;
            }

            if (node is ILRet ret)
            {
                if (ret.Operand != null)
                {
                    var expr = ret.Operand;
                    expr.RemoveSelfInParent();
                    var stat = new ReturnStatement(expr);
                    ret.ReplaceSelfInParent(stat);
                }
                else
                {
                    var stat = new ReturnStatement();
                    ret.ReplaceSelfInParent(stat);
                }
            }
        }
    }
}
