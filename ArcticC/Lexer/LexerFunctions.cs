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
    }
}
