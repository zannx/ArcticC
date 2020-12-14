using System;
using System.Collections.Generic;
using System.Text;

namespace ArcticC.StringCompiler
{
    public class CompilerStr
    {
        public static byte[] CompilerString(string operation)
        {
            char[] ArrayOperation = operation.ToCharArray();
            byte[] ConvertedVersion = new byte[ArrayOperation.Length];

            for (int i = 0; i <= operation.Length - 1; i++)
            {
                ConvertedVersion[i] = Convert.ToByte(ArrayOperation[i]);
            }
            return ConvertedVersion;
        }

        public static bool CheckBytes(byte[] First, byte[] Second)
        {
            bool AreTheyTheSame = false;

            if (First.Length == Second.Length)
            {
                for (int i = 0; i <= First.Length - 1; i++)
                {
                    if (First[i] != Second[i])
                    {
                        AreTheyTheSame = false;
                        break;
                    }
                    else
                    {
                        AreTheyTheSame = true;
                    }
                }
            }

            return AreTheyTheSame;
        }
    }
}
