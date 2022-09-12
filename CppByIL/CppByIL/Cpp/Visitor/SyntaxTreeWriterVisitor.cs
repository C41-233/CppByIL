using CppByIL.Cpp.Syntax;
using CppByIL.Cpp.Syntax.Expressions;
using CppByIL.Cpp.Syntax.IL;
using CppByIL.Cpp.Syntax.Precessor;
using CppByIL.Cpp.Syntax.Statements;
using CppByIL.Cpp.Syntax.Types;

namespace CppByIL.Cpp.Visitor
{
    public class SyntaxTreeWriterVisitor : TextWriterVisitor
    {
        public SyntaxTreeWriterVisitor(TextWriter writer) : base(writer)
        {
        }

        private void DoWrite(SyntaxNode node, string? content = null)
        {
            EnsureNewLine();
            Write(node.GetType().Name);
            if (content != null)
            {
                Write(" ");
                Write(content);
            }
            PushPad();
            VisitChild(node);
            PopPad();
        }

        public override void VisitAssignmentExpression(AssignmentExpression node)
        {
            DoWrite(node);
        }

        public override void VisitBinaryExpression(RightValueBinaryExpression node)
        {
            DoWrite(node);
        }

        public override void VisitClassDeclaration(ClassDeclaration node)
        {
            DoWrite(node);
        }

        public override void VisitExpressionStatement(ExpressionStatement node)
        {
            DoWrite(node);
        }

        public override void VisitILInstruction(ILInstruction node)
        {
            if (node is ILLocalLoad ld)
            {
                DoWrite(node, $"{ld.Variable.Name}");
            }
            else if (node is ILBinaryNumericInstruction bi)
            {
                DoWrite(node, $"{bi.Operator}");
            }
            else
            {
                DoWrite(node);
            }
        }

        public override void VisitILStatement(ILStatement node)
        {
            if (node is ILLocalStore st)
            {
                DoWrite(node, $"{st.Variable.Name}");
            }
            else if (node is ILGoto gt)
            {
                DoWrite(node, $"{gt.Target.Name}");
            }
            else if (node is ILBlock block)
            {
                DoWrite(node, $"{block.Name}");
            }
            else
            {
                DoWrite(node);
            }
        }

        public override void VisitIncludePrecessor(Include node)
        {
            DoWrite(node, $"{node.FileName}");
        }

        public override void VisitLocalVariableDeclareStatement(LocalVariableDeclareStatement node)
        {
            DoWrite(node, $"{node.Name}");
        }

        public override void VisitLocalVariableReadExpression(LocalVariableReadExpression node)
        {
            DoWrite(node, $"{node.Name}");
        }

        public override void VisitLocalVariableWriteExpression(LocalVariableWriteExpression node)
        {
            DoWrite(node);
        }

        public override void VisitMethodDeclaration(MethodDeclaration node)
        {
            DoWrite(node);
        }

        public override void VisitMethodDefinition(MethodDefinition node)
        {
            DoWrite(node, $"{node.Name}");
        }

        public override void VisitMethodBodyDefinition(MethodBodyDefinition node)
        {
            DoWrite(node);
        }

        public override void VisitMethodParameter(MethodParameter node)
        {
            DoWrite(node);
        }

        public override void VisitNamespaceDeclaration(NamespaceDeclaration node)
        {
            DoWrite(node);
        }

        public override void VisitPragmaPrecessor(Pragma node)
        {
            DoWrite(node);
        }

        public override void VisitReturnStatement(ReturnStatement node)
        {
            DoWrite(node);
        }

        public override void VisitTypeReference(CppTypeReference node)
        {
            DoWrite(node);
        }
    }
}
