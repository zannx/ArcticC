using System;
using System.Collections.Generic;
using System.Text;

namespace ArticCSharp.StringCompiler
{
    public class CompilerStr
    {
        public static string CompilerString(string operation)
        {
            char[] ArrayOperation = operation.ToCharArray();
            string ConvertedVersion = "";

            for (int i = 0; i <= operation.Length - 1; i++)
            {
                ConvertedVersion = ConvertedVersion + Convert.ToInt32(ArrayOperation[i]).ToString("X");
            }
            return ConvertedVersion;
        }
    }
}
