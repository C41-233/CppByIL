using CppByIL.Cpp.Syntax;
using CppByIL.Cpp.Syntax.Expressions;
using CppByIL.Cpp.Syntax.IL;
using CppByIL.Cpp.Syntax.Statements;
using CppByIL.Cpp.Syntax.Types;

namespace CppByIL.Decompile.Transformer
{
    internal class VariableSplitTransformer : ITransformer
    {

        public void Run(SyntaxNode body, TransformContext context)
        {
            foreach (var child in body.Children)
            {
                if (TryReplace(context, child, out var to))
                {
                    child.ReplaceSelfInParent(to);
                }
            }
        }

        private bool TryReplace(TransformContext context, SyntaxNode from, out Expression? to)
        {
            if (from is LocalStore st)
            {
                var variable = st.Variable;
                var localType = context.GetLocalVariable(variable.Name);
                if (localType == null)
                {
                    localType = TypeReference.Get(variable.Type);
                    context.SetLocalVariable(variable.Name, localType);
                }
                var left = new LocalVariableExpression(variable.Name);

                Expression right;
                if (TryReplace(context, st.Instruction, out var inst))
                {
                    right = inst!;
                }
                else
                {
                    right = new ILExpression(st.Instruction);
                }

                var expr = new AssignmentExpression(left, right);

                to = expr;
                return true;
            }
            else if (from is LocalLoad ld)
            {
                var variable = ld.Variable;
                var expr = new LocalVariableExpression(variable.Name);

                to = expr;
                return true;
            }

            to = null;
            return false;
        }

    }
}
