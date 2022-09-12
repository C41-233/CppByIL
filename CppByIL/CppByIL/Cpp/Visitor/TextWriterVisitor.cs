using CppByIL.Cpp.Syntax;

namespace CppByIL.Cpp.Visitor
{

    public abstract class TextWriterVisitor : Visitor
    {
        private readonly TextWriter writer;

        private bool startOfNewLine = true;
        private bool padOfNewLine = false;
        private int pad;

        protected TextWriterVisitor(TextWriter writer)
        {
            this.writer = writer;
        }

        protected void Write(string text)
        {
            if (!padOfNewLine)
            {
                padOfNewLine = true;
                WritePad();
            }
            writer.Write(text);
            startOfNewLine = false;
        }

        protected void EnsureNewLine()
        {
            if (!startOfNewLine)
            {
                NewLine();
            }
        }

        //换行
        protected void NewLine()
        {
            writer.WriteLine();
            startOfNewLine = true;
            padOfNewLine = false;
        }

        //保证空一行
        protected void ForceNewLine()
        {
            EnsureNewLine();
            NewLine();
        }

        private void WritePad()
        {
            for (var i = 0; i < pad; i++)
            {
                writer.Write('\t');
            }
            padOfNewLine = true;
        }

        protected void PushPad()
        {
            pad++;
        }

        protected void PopPad()
        {
            pad--;
        }

        protected void VisitChild(SyntaxNode node)
        {
            foreach (var child in node.Children)
            {
                child.Visit(this);
            }
        }

    }

}
