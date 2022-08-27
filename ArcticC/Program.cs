using System;
using static ArcticC.StringCompiler.CompilerStr;
using static ArcticC.StringCompiler.ByteArrays;
using static ArcticC.Lexer.Lexer;
using static ArcticC.Parser.Parser;
using static ArcticC.Evaluator.Evaluator;

namespace ArcticC
{

    public static class Program
    {
        public static string[][] LexeredTable;
        public static void Main(string[] args)
        {
            Console.Title = "ArcticC";

            //Basic variables
            string SourceMain = "";
            while (true)
            {
                string Input = Convert.ToString(Console.ReadLine());
                SourceMain = SourceMain + Convert.ToString('\x20');

                if (CheckBytes(CompilerString(Input.ToLower()), lexer))
                {
                    try
                    {
                        char[] SourceMainChar = SourceMain.ToCharArray();

                        //Run C# code and return result
                        LexeredTable = LexerCheck(SourceMainChar);

                        for (int i = 0; i <= LexeredTable[0].Length - 1; i++)
                        {
                            Console.WriteLine("(" + LexeredTable[0][i].ToString() + ", " + LexeredTable[1][i].ToString() + ")");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error C#: ", e);
                    }
                }
                else
                {
                    //try
                    //{
                        if (CheckBytes(CompilerString(Input.ToLower()), run))
                        {
                            var watch = new System.Diagnostics.Stopwatch();
                            watch.Start();
                            char[] SourceMainChar = SourceMain.ToCharArray();
                            LexeredTable = LexerCheck(SourceMainChar);
                            Console.WriteLine("LEXER");
                            Console.WriteLine("--------------------------------------");
                            Console.WriteLine("");
                            for (int i = 0; i <= LexeredTable[0].Length - 1; i++)
                            {
                                Console.WriteLine("(" + LexeredTable[0][i].ToString() + ", " + LexeredTable[1][i].ToString() + ")");
                            }
                            Console.WriteLine("");
                            string Parser = ParserCheck(LexeredTable);
                            Console.WriteLine("PARSER");
                            Console.WriteLine("--------------------------------------");
                            Console.WriteLine(Parser);
                            Console.WriteLine("");
                            Console.WriteLine("OUTPUT");
                            Console.WriteLine("--------------------------------------");
                            EvaluatorVoid(Parser);
                            Console.WriteLine("--------------------------------------");
                            watch.Stop();
                            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
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
                    //}
                    //catch (Exception e)
                    //{
                    //    Console.WriteLine("Error C#: ", e);
                    //}
                }
            }
        }
    }
}
