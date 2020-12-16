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
        public static string[][] LexerCheck(char[] characterarray)
        {
            string[][] SourceAppart = new string[][] {new string[characterarray.Length], new string[characterarray.Length]};

            for (int i = 0; i <= characterarray.Length - 1; i++)
            {
                byte One = (byte)characterarray[i];

                //Operators
                if (ContainsByte(operators, One))
                {
                    string Together = characterarray[i].ToString();
                    SourceAppart[1][i] = Together;
                    SourceAppart[0][i] = "operator";
                    if (ContainsByte(operators, One))
                    {
                        Together = Together + characterarray[i + 1].ToString();
                        SourceAppart[0][i] = "operator";
                        SourceAppart[1][i] = Together;
                        i++;
                    }
                }

                //Integer or float
                if (ContainsByte(integers, One))
                {
                    string Together = characterarray[i].ToString();
                    SourceAppart[0][i] = "integer";

                    int Count = 1;
                    byte NewOne = (byte)(characterarray[i] + Count);
                    while (ContainsByte(integers, NewOne) || ContainsByte(dot, NewOne))
                    {
                        if (ContainsByte(dot, NewOne))
                        {
                            Together = Together + characterarray[i + 1].ToString();
                            SourceAppart[1][i] = Together;
                            SourceAppart[0][i] = "float";
                            Count = Count + 1;
                            NewOne = (byte)(characterarray[i] + Count);
                            i++;
                            continue;
                        }
                        Together = Together + characterarray[i + 1].ToString();
                        SourceAppart[1][i] = Together;
                        Count = Count + 1;
                        NewOne = (byte)(characterarray[i] + Count);
                        i++;
                    }
                }

                //Nothing
                if (ContainsByte(nothing, One))
                {
                    string Together = "-";
                    SourceAppart[0][i] = Together;
                }

            }
            SourceAppart[0] = SourceAppart[0].Where(x => !string.IsNullOrEmpty(x)).ToArray();
            SourceAppart[1] = SourceAppart[1].Where(x => !string.IsNullOrEmpty(x)).ToArray();
            return SourceAppart;
        }
    }
}
