using CppByIL.Cpp.Syntax;
using CppByIL.Cpp.Syntax.Precessor;
using CppByIL.Cpp.Syntax.Types;

namespace CppByIL.Cpp.Visitor
{
    public class WriterVisitor : ISynctaxNodeVisitor
    {

        private readonly TextWriter writer;

        private bool startOfNewLine = true;
        private bool padOfNewLine = false;
        private int pad;

        public WriterVisitor(TextWriter writer)
        {
            this.writer = writer;
        }

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

        public void VisitMethodDeclaration(MethodDeclaration method)
        {
            EnsureNewLine();

            Write("PUBLIC");
            Space();
            if (method.IsStatic)
            {
                Write("static");
                Space();
            }
            method.ReturnType.Visit(this);
            Space();
            Write(method.Name);
            Write("(");
            {
                var first = true;
                foreach (var parameter in method.ParameterDeclarations)
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

        public void VisitMethodParameterDeclaration(MethodParameterDeclaration node)
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

        private void Write(string text)
        {
            if (!padOfNewLine)
            {
                padOfNewLine = true;
                WritePad();
            }
            writer.Write(text);
            startOfNewLine = false;
        }

        private void EnsureNewLine()
        {
            if (!startOfNewLine)
            {
                NewLine();
            }
        }

        //换行
        private void NewLine()
        {
            writer.WriteLine();
            startOfNewLine = true;
            padOfNewLine = false;
        }

        //保证空一行
        private void ForceNewLine()
        {
            EnsureNewLine();
            NewLine();
        }

        private void WritePad()
        {
            for (var i=0; i<pad; i++)
            {
                writer.Write('\t'); 
            }
            padOfNewLine = true;
        }

        private void OpenBrace()
        {
            EnsureNewLine();
            Write("{");
        }

        private void CloseBrace()
        {
            EnsureNewLine();
            Write("}");
        }

        private void Semicolon()
        {
            Write(";");
        }

        private void Comma()
        {
            Write(",");
        }

        private void Space()
        {
            Write(" ");
        }

        private void PushPad()
        {
            pad++;
        }

        private void PopPad()
        {
            pad--;
        }

    }
}
