using CppByIL.Cpp.Syntax.Statements;

namespace CppByIL.Cpp.Syntax.Expressions
{
    public abstract class Expression : SyntaxNode
    {
    }

    public abstract class LeftValueExpression : Expression
    {

    }

    public abstract class RightValueExpression : Expression 
    { 
    }
}
