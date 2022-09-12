using CppByIL.Cpp.Syntax;
using CppByIL.Cpp.Visitor;

public class NamespaceDeclaration : SyntaxNode
{

    public string Value { get; }

    public NamespaceDeclaration(string value)
    {
        Value = value;
    }

    public override void Visit(Visitor visitor)
    {
        visitor.VisitNamespaceDeclaration(this);
    }
}