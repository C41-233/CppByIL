using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CppByIL.Decompile
{
    internal static class ILParser
    {

        public static ILOpCode DecodeOpCode(ref BlobReader reader)
        {
            byte opCodeByte = reader.ReadByte();
            return (ILOpCode)(opCodeByte == 0xFE ? 0xFE00 + reader.ReadByte() : opCodeByte);
        }

        public static int DecodeBranchTarget(ref BlobReader reader, ILOpCode opCode)
        {
            return (opCode.GetBranchOperandSize() == 4 ? reader.ReadInt32() : reader.ReadSByte()) + reader.Offset;
        }

    }
}
