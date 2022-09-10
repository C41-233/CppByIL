using CppByIL.Cpp.Syntax;
using CppByIL.Cpp.Syntax.Precessor;
using CppByIL.Cpp.Syntax.Types;

namespace CppByIL.Cpp.Visitor
{
    public partial class WriterVisitor
    {

        private readonly TextWriter writer;

        private bool startOfNewLine = true;
        private bool padOfNewLine = false;
        private int pad;

        public WriterVisitor(TextWriter writer)
        {
            this.writer = writer;
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
            EnsureNewLine();
            PushPad();
        }

        private void CloseBrace()
        {
            PopPad();
            EnsureNewLine();
            Write("}");
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
