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
                Console.ForegroundColor = ConsoleColor.White;
                string Input = Convert.ToString(Console.ReadLine());
                SourceMain = SourceMain + Convert.ToString('\x20');

                if (CheckBytes(CompilerString(Input.ToLower()), lexer))
                {
                    char[] SourceMainChar = SourceMain.ToCharArray();
                   
                    //Run C# code and return result
                    string[][] LexeredTable = LexerCheck(SourceMainChar);

                    try
                    {
                        for (int i = 0; i <= LexeredTable[0].Length - 1; i++)
                        {
                            Console.WriteLine("(" + LexeredTable[0][i].ToString() + ", " + LexeredTable[1][i].ToString() + ")");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Error of C#: {0}", e);
                        Console.ForegroundColor = ConsoleColor.White;
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
