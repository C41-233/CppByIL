using CppByIL.Cpp.Syntax.IL;
using CppByIL.ILMeta.TypeSystem;
using System.Reflection.Metadata;

namespace CppByIL.Decompile
{
    partial class ILMethodBodyDecompiler
    {

        private ILStatement Nop()
        {
            return new ILNop();
        }

        private ILStatement Ldarg(int index)
        {
            return Push(new ILLocalLoad(parameters[index]));
        }

        private ILStatement Ldloc(int index)
        {
            return Push(new ILLocalLoad(locals[index]));
        }

        private ILStatement Stloc(int index)
        {
            return new ILLocalStore(locals[index], Pop());
        }

        private ILStatement BinaryNumeric(BinaryNumericOperator op)
        {
            var y = Pop();
            var x = Pop();
            return Push(new ILBinaryNumericInstruction(op, x, y));
        }

        private ILStatement Br(ref BlobReader reader, ILOpCode code)
        {
            var offset = ILParser.DecodeBranchTarget(ref reader, code);
            return new ILBranch(offset);
        }

        private ILStatement Ret()
        {
            if (method.ReturnType == ILPrimitiveTypeReference.Void)
            {
                return new ILRet();
            }
            else
            {
                return new ILRet(Pop());
            }
        }

    }
}
