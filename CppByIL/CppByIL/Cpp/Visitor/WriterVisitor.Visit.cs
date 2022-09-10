using CppByIL.Cpp.Syntax;
using CppByIL.Cpp.Syntax.Precessor;
using CppByIL.Cpp.Syntax.Types;

namespace CppByIL.Cpp.Visitor
{
    public partial class WriterVisitor
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
            var ns = node.Value.Replace(".", "::");
            Write($"namespace {ns}");
            OpenBrace();
            PushPad();
            ForceNewLine();
            VisitChild(node);
            ForceNewLine();
            PopPad();
            CloseBrace();
        }

        public void VisitClassDeclaration(ClassDeclaration classDeclaration)
        {
            EnsureNewLine();
            Write($"class {classDeclaration.Name}");
            OpenBrace();
            PushPad();

            NewLine();
            VisitChild(classDeclaration);
            NewLine();

            PopPad();
            CloseBrace();
            Semicolon();
        }

        public void VisitMethodDefinition(MethodDefinition node)
        {
            ForceNewLine();
            node.ReturnType.Visit(this);
            Space();
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
                        Comma();
                        Space();
                    }
                    parameter.Visit(this);
                }
            }
            Write(")");
            NewLine();
            OpenBrace();
            PushPad();
            NewLine();
            {
                Write("return 0;");
            }
            PopPad();
            NewLine();
            CloseBrace();
        }

        public void VisitMethodDeclaration(MethodDeclaration node)
        {
            EnsureNewLine();

            Write("PUBLIC");
            Space();
            if (node.IsStatic)
            {
                Write("static");
                Space();
            }
            node.ReturnType.Visit(this);
            Space();
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
                        Comma();
                        Space();
                    }
                    parameter.Visit(this);
                }
            }
            Write(")");
            Semicolon();
        }

        public void VisitMethodParameter(MethodParameter node)
        {
            node.ParameterType.Visit(this);
            Space();
            Write(node.Name);
        }

        public void VisitTypeReference(TypeReference type)
        {
            Write(type.FullName);
        }

        private void VisitChild(SyntaxNode node)
        {
            foreach (var child in node.Children)
            {
                child.Visit(this);
            }
        }

    }
}
