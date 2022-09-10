using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppByIL.Cpp.Syntax
{
    public static class SyntaxUtils
    {

        public static string ILFullNameToCppFullName(string value)
        {
            var sb = new StringBuilder();
            foreach (var ch in value)
            {
                if (ch == '.')
                {
                    sb.Append("::");
                }
                else
                {
                    AppendCppCharacter(sb, ch);
                }
            }
            return sb.ToString();
        }

        public static string ILFullNameToCppPathName(string value)
        {
            var sb = new StringBuilder();
            foreach (var ch in value)
            {
                if (ch == '.')
                {
                    sb.Append(".");
                }
                else
                {
                    AppendCppCharacter(sb, ch);
                }
            }
            return sb.ToString();
        }

        private static void AppendCppCharacter(StringBuilder sb, char ch)
        {
            if (IsCppCharacter(ch))
            {
                sb.Append(ch);
            }
            else
            {
                sb.Append($"u{(int)ch}");
            }
        }

        private static bool IsCppCharacter(char ch)
        {
            if (ch == '_')
            {
                return true;
            }
            if (ch >= '0' && ch <= '9')
            {
                return true;
            }
            if (ch >= 'a' && ch <= 'z')
            {
                return true;
            }
            if (ch >= 'A' && ch <= 'Z')
            {
                return true;
            }
            return false;
        }

    }
}
