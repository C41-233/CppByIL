using CppByIL.Decompile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppByIL.Cpp.Syntax.IL
{
    public class LocalStore : ILInstruction
    {

        private ILVariable variable;
        private ILInstruction inst;

        internal LocalStore(ILVariable varibale, ILInstruction inst)
        {
            this.variable = varibale;
            this.inst = inst;
        }

        public override string ToString()
        {
            return $"IL_Store({variable.Name}, {inst})";
        }
    }
}
