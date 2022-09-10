using CppByIL.Cpp.Syntax;

namespace CppByIL.Decompile.Transformer
{
    internal interface ITransformer
    {
        void Run(SyntaxNode body, TransformContext context);
    }
}
