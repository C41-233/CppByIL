using CppByIL.Cpp.Syntax;
using CppByIL.Cpp.Syntax.Expressions;
using CppByIL.Cpp.Syntax.Statements;
using CppByIL.Cpp.Syntax.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppByIL.Decompile.Transformer
{

    //在合适的位置（第一次写变量）插入局部变量的声明
    internal class LocalVariableDeclarationTransformer : ITransformer
    {

        public void Run(MethodBodyDefinition body, TransformContext context)
        {
            var declared = new HashSet<string>();
            foreach (var statement in body.Statements)
            {
                Walk(statement, declared);
            }
        }

        private void Walk(SyntaxNode node, HashSet<string> declared)
        {
            foreach (var child in node.Children)
            {
                Walk(child, declared);
            }

            if (node is ExpressionStatement stat
                && stat.Expression is AssignmentExpression assign 
                && assign.LeftValue is LocalVariableWriteExpression local
            )
            {
                if (declared.Add(local.Name))
                {
                    var right = assign.RightValue;
                    right.RemoveSelfInParent();

                    var replace = new LocalVariableDeclareStatement(
                        local.Name, 
                        local.Type,
                        right
                    );
                    node.ReplaceSelfInParent(replace);
                }
            }
        }
    }
}
