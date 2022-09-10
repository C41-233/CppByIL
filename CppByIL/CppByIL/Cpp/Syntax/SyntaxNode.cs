using CppByIL.Cpp.Visitor;
using System.Diagnostics;

namespace CppByIL.Cpp.Syntax
{
    public abstract class SyntaxNode
    {

        public SyntaxNode? FirstChild { get; private set; }
        public SyntaxNode? LastChild { get; private set; }
        public SyntaxNode? Prevsibling { get; private set; }
        public SyntaxNode? NextSibling { get; private set; }
        public SyntaxNode? Parent { get; private set; }

        public void AppendChild(SyntaxNode node)
        {
            if (node.Parent != null)
            {
                throw new InvalidOperationException();
            }

            node.Parent = this;
            if (FirstChild == null)
            {
                FirstChild = LastChild = node;
            }
            else
            {
                Debug.Assert(LastChild != null);
                LastChild.NextSibling = node;
                node.Prevsibling = LastChild;
                LastChild = node;
            }
        }

        public void ReplaceSelfInParent(SyntaxNode node)
        {
            if (Parent == null)
            {
                return;
            }
            Parent.ReplaceChild(this, node);
        }

        private void ReplaceChild(SyntaxNode from, SyntaxNode to)
        {
            if (from.Parent != this)
            {
                throw new InvalidOperationException();
            }
            if (to.Parent != null)
            {
                throw new InvalidOperationException();
            }
            from.Parent = null;
            to.Parent = this;

            to.Prevsibling = from.Prevsibling;
            to.NextSibling = from.NextSibling;

            if (from.Prevsibling != null)
            {
                from.Prevsibling.NextSibling = to;
            }
            if (from.NextSibling != null)
            {
                from.NextSibling.Prevsibling = to;
            }

            to.FirstChild = from.FirstChild;
            to.LastChild = from.LastChild;

            if (FirstChild == from)
            {
                FirstChild = to;
            }
            if (LastChild == from)
            {
                LastChild = to;
            }
        }

        public void RemoveSelfInParent()
        {
            if (Parent == null)
            {
                return;
            }
            Parent.RemoveChild(this);
        }

        public void RemoveChild(SyntaxNode node)
        {
            if (node.Parent != this)
            {
                throw new InvalidOperationException();
            }
            node.Parent = null;
            var next = node.NextSibling;
            node.NextSibling = null;
            var prev = node.Prevsibling;
            node.Prevsibling = null;

            if (prev != null)
            {
                prev.NextSibling = next;
            }
            if (next != null)
            {
                next.Prevsibling = prev;
            }

            if (FirstChild == node)
            {
                FirstChild = next;
            }
            if (LastChild == node)
            {
                LastChild = prev;
            }
        }

        public IEnumerable<SyntaxNode> Children
        {
            get
            {
                var node = FirstChild;
                while (node != null)
                {
                    var next = node.NextSibling;
                    yield return node;
                    node = next;
                }
            }
        }

        public abstract void Visit(ISynctaxNodeVisitor visitor);

    }
}
