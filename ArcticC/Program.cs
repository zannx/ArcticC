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

                if (CheckBytes(CompilerString(Input.ToLower()), compile))
                {
                    char[] SourceMainChar = SourceMain.ToCharArray();
                    //string[,] SourceAppart = new string[SourceMainChar.Length,SourceMainChar.Length];

                    //int Count = 0;
                    //string Object = "";
                    //for (int i = 0; i <= SourceMainChar.Length - 1; i++)
                    //{

                    //    if (SourceMainChar[i] == 0x20)
                    //    {

                    //        ////End
                    //        //if (CheckBytes(CompilerString(Object.ToLower()), end))
                    //        //{
                    //        //    Console.WriteLine(Object.ToLower());
                    //        //}

                    //        SourceAppart[1, Count] = Object; 
                    //        Count = Count + 1;

                    //        //Check string what it is and convert it to C#
                    //        Console.WriteLine(Object.ToString());

                    //        Object = "";
                    //        continue;
                    //    }
                    //    Object = Object + SourceMainChar[i].ToString();
                    //}

                    //Run C# code and return result
                    string[][] LexeredTable = LexerCheck(SourceMainChar);

                    for (int i = 0; i <= LexeredTable.Length - 1; i++)
                    {
                        Console.WriteLine(LexeredTable[0][i].ToString() + ", " + LexeredTable[1][i].ToString());
                    }
        
                }
                else
                {
                    SourceMain = Input;
                }
            }
        }
    }
}
