using CppByIL.Cpp.Syntax;
using CppByIL.Cpp.Syntax.IL;
using CppByIL.Cpp.Syntax.Precessor;
using CppByIL.Cpp.Syntax.Statements;
using CppByIL.Cpp.Syntax.Types;

namespace CppByIL.Cpp.Visitor
{
    public partial class WriterVisitor : ISynctaxNodeVisitor
    {

        public void VisitIncludePrecessor(Include node)
        {
            EnsureNewLine();
            Write($"#include <{node.FileName}>");
            NewLine();
        }

        public void VisitPragmaPrecessor(Pragma node)
        {
            EnsureNewLine();
            Write($"#pragma {node.Value}");
            NewLine();
        }

        public void VisitNamespaceDeclaration(NamespaceDeclaration node)
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

        public void VisitClassDeclaration(ClassDeclaration classDeclaration)
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

        public void VisitMethodDefinition(MethodDefinition node)
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

        public void VisitMethodDeclaration(MethodDeclaration node)
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

        public void VisitMethodParameter(MethodParameter node)
        {
            node.ParameterType.Visit(this);
            Write(" ");
            Write(node.Name);
        }

        public void VisitTypeReference(TypeReference type)
        {
            Write(type.FullName);
        }

        public void VisitMethodBody(MethodBody node)
        {
            OpenBrace();
            VisitChild(node);
            CloseBrace();
        }

        private void VisitChild(SyntaxNode node)
        {
            foreach (var child in node.Children)
            {
                child.Visit(this);
            }
        }

        public void VisitILInstruction(ILInstruction node)
        {
            EnsureNewLine();
            Write(node.ToString()!);
        }
    }
}
