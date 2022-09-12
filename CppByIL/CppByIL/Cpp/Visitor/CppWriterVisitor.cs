namespace CppByIL.Cpp.Visitor
{
    public partial class CppWriterVisitor : TextWriterVisitor
    {

        public CppWriterVisitor(TextWriter writer) : base(writer)
        {
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

    }
}
