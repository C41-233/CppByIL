using CppByIL.Cpp.Syntax;
using CppByIL.Cpp.Syntax.Expressions;
using CppByIL.Cpp.Syntax.IL;
using CppByIL.Cpp.Syntax.Precessor;
using CppByIL.Cpp.Syntax.Statements;
using CppByIL.Cpp.Syntax.Types;

namespace CppByIL.Cpp.Visitor
{
    public partial class CppWriterVisitor : TextWriterVisitor
    {

        public override void VisitILStatement(ILStatement node)
        {
            EnsureNewLine();
            Write(node.ToString()!);
            EnsureNewLine();

        }
        public override void VisitILInstruction(ILInstruction node)
        {
            Write(node.ToString()!);
        }

        public override void VisitIncludePrecessor(Include node)
        {
            EnsureNewLine();
            Write($"#include <{node.FileName}>");
            NewLine();
        }

        public override void VisitPragmaPrecessor(Pragma node)
        {
            EnsureNewLine();
            Write($"#pragma {node.Value}");
            NewLine();
        }

        public override void VisitNamespaceDeclaration(NamespaceDeclaration node)
        {
            EnsureNewLine();
            NewLine();
            Write($"namespace {node.Value}");
            OpenBrace();
            ForceNewLine();
            VisitChild(node);
            ForceNewLine();
            CloseBrace();
        }

        public override void VisitClassDeclaration(ClassDeclaration classDeclaration)
        {
            EnsureNewLine();
            Write($"class {classDeclaration.Name}");
            OpenBrace();

            NewLine();
            VisitChild(classDeclaration);
            NewLine();

            CloseBrace();
            Write(";");
        }

        public override void VisitMethodDefinition(MethodDefinition node)
        {
            ForceNewLine();
            node.ReturnType.Visit(this);
            Write(" ");
            if (node.DeclaringType != null)
            {
                node.DeclaringType.Visit(this);
                Write("::");
                Write(node.Name);
            }
            else
            {
                Write(node.Name);
            }
            Write("(");
            {
                var first = true;
                foreach (var parameter in node.ParameterList)
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        Write(", ");
                    }
                    parameter.Visit(this);
                }
            }
            Write(")");
            node.MethodBody.Visit(this);
        }

        public override void VisitMethodDeclaration(MethodDeclaration node)
        {
            EnsureNewLine();

            Write("PUBLIC");
            Write(" ");
            if (node.IsStatic)
            {
                Write("static");
                Write(" ");
            }
            node.ReturnType.Visit(this);
            Write(" ");
            Write(node.Name);
            Write("(");
            {
                var first = true;
                foreach (var parameter in node.ParameterList)
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        Write(", ");
                    }
                    parameter.Visit(this);
                }
            }
            Write(")");
            Write(";");
        }

        public override void VisitMethodParameter(MethodParameter node)
        {
            node.ParameterType.Visit(this);
            Write(" ");
            Write(node.Name);
        }

        public override void VisitTypeReference(CppTypeReference type)
        {
            Write(type.FullName);
        }

        public override void VisitAssignmentExpression(AssignmentExpression node)
        {
            node.LeftValue.Visit(this);
            Write(" = ");
            node.RightValue.Visit(this);
        }

        public override void VisitLocalVariableReadExpression(LocalVariableReadExpression node)
        {
            Write(node.Name);
        }

        public override void VisitExpressionStatement(ExpressionStatement node)
        {
            EnsureNewLine();
            node.Expression.Visit(this);
            Write(";");
            EnsureNewLine();
        }

        public override void VisitBinaryExpression(RightValueBinaryExpression node)
        {
            node.Left.Visit(this);
            Write(" ");
            switch (node.Operator)
            {
                case BinaryOperator.Add:
                    Write("+");
                    break;
                case BinaryOperator.Subtract:
                    Write("-");
                    break;
                case BinaryOperator.Multiply:
                    Write("*");
                    break;
            }
            Write(" ");
            node.Right.Visit(this);
        }

        public override void VisitLocalVariableWriteExpression(LocalVariableWriteExpression node)
        {
            Write(node.Name);
        }

        public override void VisitReturnStatement(ReturnStatement node)
        {
            EnsureNewLine();
            Write("return");
            if (node.Expression != null)
            {
                Write(" ");
                node.Expression.Visit(this);
            }
            Write(";");
        }

        public override void VisitLocalVariableDeclareStatement(LocalVariableDeclareStatement node)
        {
            EnsureNewLine();
            Write(node.Type.FullName);
            Write(" ");
            Write(node.Name);
            Write(" = ");
            if (node.Expression != null)
            {
                node.Expression.Visit(this);
            }
            Write(";");
        }

        public override void VisitMethodBodyDefinition(MethodBodyDefinition node)
        {
            OpenBrace();
            VisitChild(node);
            CloseBrace();
        }

    }
}
