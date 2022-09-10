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

    }
}
