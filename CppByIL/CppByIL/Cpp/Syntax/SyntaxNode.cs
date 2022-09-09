using CppByIL.Cpp.Visitor;
using System.Diagnostics;

namespace CppByIL.Cpp.Syntax
{
    public abstract class SyntaxNode
    {

        private SyntaxNode? firstChild;
        private SyntaxNode? lastChild;
        private SyntaxNode? nextSibling;

        public void AppendChild(SyntaxNode node)
        {
            if (firstChild == null)
            {
                firstChild = lastChild = node;
            }
            else
            {
                Debug.Assert(lastChild != null);
                lastChild.nextSibling = node;
                lastChild = node;
            }
        }

        public IEnumerable<SyntaxNode> Children
        {
            get
            {
                var node = firstChild;
                while (node != null)
                {
                    yield return node;
                    node = node.nextSibling;
                }
            }
        }

        public abstract void Visit(ISynctaxNodeVisitor visitor);

    }
}
