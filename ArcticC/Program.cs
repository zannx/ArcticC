using System;
using static ArcticC.StringCompiler.CompilerStr;
using static ArcticC.StringCompiler.ByteArrays;
using static ArcticC.Lexer.Lexer;

namespace ArcticC
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "ArcticC";

            //Basic variables
            string SourceMain = "";
            string Assembly = "";

            while (true)
            { 
                string Input = Convert.ToString(Console.ReadLine());
                SourceMain = SourceMain + Convert.ToString('\x20');

                if (CheckBytes(CompilerString(Input.ToLower()), lexer))
                {
                    char[] SourceMainChar = SourceMain.ToCharArray();
                   
                    //Run C# code and return result
                    string[][] LexeredTable = LexerCheck(SourceMainChar);

                    for (int i = 0; i <= LexeredTable[0].Length - 1; i++)
                    {
                        Console.WriteLine("(" + LexeredTable[0][i].ToString() + ", " + LexeredTable[1][i].ToString() + ")");
                    }
                }
                else
                {
                    if (SourceMain != "")
                    {
                        SourceMain = SourceMain + Input;
                    }
                    else
                    {
                        SourceMain = Input;
                    }
                }
            }
        }
    }
}
