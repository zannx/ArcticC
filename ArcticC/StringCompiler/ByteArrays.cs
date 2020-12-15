using System;
using System.Collections.Generic;
using System.Text;

namespace ArcticC.StringCompiler
{
    public class ByteArrays
    {
        public static byte[] end = {0x65, 0x6E, 0x64};
        public static byte[] compile = {0x63, 0x6F, 0x6D, 0x70, 0x69, 0x6c, 0x65};
        public static byte[] operators = {0x2B, 0x2D, 0x2A, 0x2F};
        public static byte[] nothing = {0x3D, 0x3B};
        public static byte[] integers = {0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39};
        public static byte[] dot = {0x2E};
    }
}
