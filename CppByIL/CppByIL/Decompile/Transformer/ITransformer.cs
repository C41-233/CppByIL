using CppByIL.Cpp.Syntax;
using CppByIL.Cpp.Syntax.Statements;
using CppByIL.Cpp.Syntax.Types;

namespace CppByIL.Decompile.Transformer
{
    internal interface ITransformer
    {
        void Run(MethodBodyDefinition body, TransformContext context);
    }
}
