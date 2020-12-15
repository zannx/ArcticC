using System;
using System.Collections.Generic;
using System.Text;
using static ArcticC.StringCompiler.CompilerStr;
using static ArcticC.StringCompiler.ByteArrays;
using System.Linq;

namespace ArcticC.Lexer
{
    public class Lexer
    {
        public static string[,] LexerCheck(char[] characterarray)
        {
            string[,] SourceAppart = new string[characterarray.Length, characterarray.Length];

            for (int i = 0; i <= characterarray.Length - 1; i++)
            { 
                //Operators
                if (operators.Contains((byte)characterarray[i]))
                {
                    string Together = characterarray[i].ToString();
                    SourceAppart[1, i] = Together;
                    SourceAppart[0, i] = "operator";
                    if (operators.Contains((byte)characterarray[i + 1]))
                    {
                        Together = Together + characterarray[i + 1].ToString();
                        SourceAppart[0, i] = "operator";
                        SourceAppart[1, i] = Together;
                        i++;
                    }
                }

                //Integer or float
                if (integers.Contains((byte)characterarray[i]))
                {
                    string Together = characterarray[i].ToString();
                    SourceAppart[0, i] = "integer";

                    int Count = 1;
                    while (operators.Contains((byte)characterarray[i + Count]) || dot.Contains((byte)characterarray[i + Count]))
                    {
                        if (dot.Contains((byte)characterarray[i + Count]))
                        {
                            Together = Together + characterarray[i + 1].ToString();
                            SourceAppart[1, i] = Together;
                            SourceAppart[0, i] = "float";
                            i++;
                            continue;
                        }
                        Together = Together + characterarray[i + 1].ToString();
                        SourceAppart[1, i] = Together;
                        Count = Count + 1;
                        i++;
                    }
                }

                //Nothing
                if (operators.Contains((byte)characterarray[i]))
                {
                    string Together = characterarray[i].ToString();
                    SourceAppart[0, i] = Together;
                }
            }
            return SourceAppart;
        }
    }
}
