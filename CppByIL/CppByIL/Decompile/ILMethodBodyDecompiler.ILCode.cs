using CppByIL.Cpp.Syntax.IL;

namespace CppByIL.Decompile
{
    partial class ILMethodBodyDecompiler
    {

        private ILInstruction Nop()
        {
            return new Nop();
        }

        private ILInstruction Ldarg(int index)
        {
            return Push(new LocalLoad(parameters[index]));
        }

        private ILInstruction Ldloc(int index)
        {
            return Push(new LocalLoad(locals[index]));
        }

        private ILInstruction Stloc(int index)
        {
            return new LocalStore(locals[index], Pop());
        }

        private ILInstruction BinaryNumeric(BinaryNumericOperator op)
        {
            var y = Pop();
            var x = Pop();
            return Push(new BinaryNumericInstruction(op, x, y));
        }

    }
}
