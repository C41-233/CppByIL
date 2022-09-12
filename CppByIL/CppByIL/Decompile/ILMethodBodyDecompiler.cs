using CppByIL.Cpp.Syntax;
using CppByIL.Cpp.Syntax.IL;
using CppByIL.Cpp.Syntax.Statements;
using CppByIL.Cpp.Syntax.Types;
using CppByIL.ILMeta;
using CppByIL.ILMeta.TypeSystem;
using CppByIL.Utils.Collections;
using System.Collections.Immutable;
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

        private ImmutableStack<ILVariable> stack;
        private int stackSlotNumber;

        private readonly Dictionary<int, ILStatement> offsets = new();

        public ILMethodBodyDecompiler(ILMethodDefinition method)
        {
            this.method = method; 
            body = method.GetMethodBody();
            genericContext = new GenericContext();
        }

        public MethodBodyDefinition Decompile()
        {
            InitParameters();
            InitLocals();

            stack = ImmutableStack<ILVariable>.Empty;
            var rootBlock = new MethodBodyDefinition();
            var reader = body.GetILReader();
            DoDecompile(ref reader, rootBlock);
            return rootBlock;
        }

        private void DoDecompile(ref BlobReader reader, MethodBodyDefinition root)
        {
            while (reader.RemainingBytes > 0)
            {
                var offset = reader.Offset;
                var instruction = DecodeNextIL(ref reader);
                offsets.Add(offset, instruction);
                root.AppendChild(instruction);
            }

            FixBranchBlocks(root);
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
                parameters[0] = new ILVariable(ILVariableKind.Parameter, method.DeclaringType, "this");
                next++;
            }
            for (var i=0; i<method.Parameters.Count; i++, next++)
            {
                var parameter = method.Parameters[i];
                parameters[next] = new ILVariable(
                    ILVariableKind.Parameter,
                    parameter.ParameterType,
                    parameter.Name
                );
            }
        }

        private void InitLocals()
        {
            var signature = method.Assembly.MetaReader.GetStandaloneSignature(body.LocalSignature);
            var locals = signature.DecodeLocalSignature(TypeProvider.Instance, genericContext);
            this.locals = new ILVariable[locals.Length];
            for (var i = 0; i < locals.Length; i++)
            {
                var local = locals[i];
                this.locals[i] = new ILVariable(ILVariableKind.Local, local, $"local{i}");
            }
        }

        private ILStatement DecodeNextIL(ref BlobReader reader)
        {
            var code = ILParser.DecodeOpCode(ref reader);
            switch (code)
            {
                case ILOpCode.Add:
                    return BinaryNumeric(BinaryNumericOperator.Add);
                case ILOpCode.Br_s:
                    return Br(ref reader, code);
                case ILOpCode.Ldarg_0:
                    return Ldarg(0);
                case ILOpCode.Ldarg_1:
                    return Ldarg(1);
                case ILOpCode.Ldloc_0:
                    return Ldloc(0);
                case ILOpCode.Mul:
                    return BinaryNumeric(BinaryNumericOperator.Mul);
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

        private ILStatement Push(ILInstruction inst)
        {
            var variable = new ILVariable(
                ILVariableKind.StackSlot, 
                ILTypeReference.Get(PrimitiveTypeCode.Int32),
                $"stack{stackSlotNumber}"
            );
            stackSlotNumber++;
            stack = stack.Push(variable);
            return new ILLocalStore(variable, inst);
        }

        private ILInstruction Pop()
        {
            stack = stack.Pop(out var variable);
            return new ILLocalLoad(variable);
        }

        private void FixBranchBlocks(MethodBodyDefinition root)
        {
            int labelNumber = 0;

            //target -> source list
            var dict = new ListDictionary<ILStatement, ILStatement>();
            foreach (ILStatement st in root.Statements)
            {
                if (st is ILBranch branch)
                {
                    var target = offsets[branch.Offset];
                    dict.Add(target, st);
                }
            }
            foreach (var (target, list) in dict)
            {
                var container = new ILBlock($"label{labelNumber}");
                labelNumber++;

                SyntaxNode? node = target.NextSibling;
                target.ReplaceSelfInParent(container);
                container.AppendChild(target);

                while (node != null)
                {
                    var next = node.NextSibling;
                    node.RemoveSelfInParent();
                    container.AppendChild(node);
                    node = next;
                }

                foreach (var branch in list)
                {
                    var gt = new ILGoto(container);
                    branch.ReplaceSelfInParent(gt);
                }
            }
        }

    }
}
