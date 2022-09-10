using CppByIL.Cpp.Syntax.IL;
using CppByIL.Cpp.Syntax.Statements;
using CppByIL.ILMeta;
using CppByIL.ILMeta.TypeSystem;
using System.Reflection.Metadata;

#pragma warning disable CS8618

namespace CppByIL.Decompile
{
    public partial class ILMethodBodyDecompiler
    {

        private ILMethodDefinition method;
        private MethodBodyBlock body;
        private GenericContext genericContext;

        private ILVariable[] locals;
        private ILVariable[] parameters;

        public ILMethodBodyDecompiler(ILMethodDefinition method)
        {
            this.method = method; 
            body = method.GetMethodBody();
            genericContext = new GenericContext();
        }

        public MethodBody Decompile()
        {
            InitParameters();
            InitVariables();

            var rootBlock = new MethodBody();
            var reader = body.GetILReader();
            while (reader.RemainingBytes > 0)
            {
                var instruction = DecodeNextIL(ref reader);
                rootBlock.AppendChild(instruction);
            }
            return rootBlock;
        }

        private void InitParameters()
        {
            var next = 0;
            if (method.IsStatic)
            {
                parameters = new ILVariable[method.Parameters.Count];
            }
            else
            {
                parameters = new ILVariable[method.Parameters.Count + 1];
                parameters[0] = new ILVariable(method.DeclaringType, "this");
                next++;
            }
            for (var i=0; i<method.Parameters.Count; i++, next++)
            {
                var parameter = method.Parameters[i];
                parameters[next] = new ILVariable(
                    parameter.ParameterType,
                    parameter.Name
                );
            }
        }

        private void InitVariables()
        {
            var signature = method.Assembly.MetaReader.GetStandaloneSignature(body.LocalSignature);
            var locals = signature.DecodeLocalSignature(TypeProvider.Instance, genericContext);
            this.locals = new ILVariable[locals.Length];
            for (var i = 0; i < locals.Length; i++)
            {
                var local = locals[i];
                this.locals[i] = new ILVariable(local, $"local{i}");
            }
        }

        private ILInstruction DecodeNextIL(ref BlobReader reader)
        {
            var code = ILParser.DecodeOpCode(ref reader);
            switch (code)
            {
                case ILOpCode.Add:
                    return BinaryNumeric(BinaryNumericOperator.Add);
                case ILOpCode.Br_s:
                    return Nop();
                case ILOpCode.Ldarg_0:
                    return Ldarg(0);
                case ILOpCode.Ldarg_1:
                    return Ldarg(1);
                case ILOpCode.Ldloc_0:
                    return Ldloc(0);
                case ILOpCode.Nop: 
                    return Nop();
                case ILOpCode.Ret:
                    return Ret();
                case ILOpCode.Stloc_0:
                    return Stloc(0);
                case ILOpCode.Sub:
                    return BinaryNumeric(BinaryNumericOperator.Sub);
                default:
                    throw new NotSupportedException(code.ToString());
            }
        }

        private ILInstruction Ret()
        {
            if (method.ReturnType == ILPrimitiveTypeReference.Void)
            {
                return new Ret();
            }
            else
            {
                return new Ret(Pop());
            }
        }

        private ILInstruction Push(ILInstruction inst)
        {
            var variable = new ILVariable(ILTypeReference.Get(PrimitiveTypeCode.Int32), "stack");
            return new LocalStore(variable, inst);
        }

        private ILInstruction Pop()
        {
            var variable = new ILVariable(ILTypeReference.Get(PrimitiveTypeCode.Int32), "pop");
            return new LocalLoad(variable);
        }
    }
}
