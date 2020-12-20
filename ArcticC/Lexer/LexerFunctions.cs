using System;
using System.Collections.Generic;
using System.Text;

namespace ArcticC.Lexer
{
    public class LexerFunctions
    {
        public static bool CheckByteSize(byte Prvi, byte Sredina, byte Drugi)
        {
            if (Prvi <= Sredina && Drugi >= Sredina)
            {
                return true;
            }
            return false;
        }

        public static byte[] GenerateByteArray(string Name)
        {
            char[] CharArray = Name.ToCharArray();
            byte[] ByteArray = new byte[Name.Length];

            for (int i = 0; i <= CharArray.Length - 1; i++)
            {
                ByteArray[i] = (byte)CharArray[i];
            }

            return ByteArray;
        }
    }
}
