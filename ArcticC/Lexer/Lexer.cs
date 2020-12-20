using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static ArcticC.StringCompiler.CompilerStr;
using static ArcticC.StringCompiler.ByteArrays;
using static ArcticC.Lexer.LexerFunctions;

namespace ArcticC.Lexer
{
    public class Lexer
    {
        public static string[][] LexerCheck(char[] characterarray)
        {
            string[][] SourceAppart = new string[][] {new string[characterarray.Length], new string[characterarray.Length]};

            int Count = 0;
            for (int i = 0; i <= characterarray.Length - 1; i++)
            {
                byte One = (byte)characterarray[i];

                //Operators
                if (ContainsByte(operators, One))
                {
                    string Together = characterarray[i].ToString();
                    SourceAppart[1][Count] = "\"" + Together + "\"";
                    SourceAppart[0][Count] = "\"operator\"";
                    if (ContainsByte(operators, (byte)characterarray[i + 1]))
                    {
                        i++;
                        Together = Together + characterarray[i].ToString();
                        SourceAppart[1][Count] = "\"" + Together + "\"";
                    }
                    Count = Count + 1;
                    continue;
                }

                //Integer or decimal
                if (ContainsByte(integers, One))
                {
                    string Together = characterarray[i].ToString();
                    SourceAppart[0][Count] = "\"integer\"";

                    if (ContainsByte(integers, (byte)characterarray[i + 1]) || (ContainsByte(dot, (byte)characterarray[i + 1])))
                    {
                        i++;
                        SourceAppart[0][Count] = "\"decimal\"";
                        while (ContainsByte(integers, (byte)(characterarray[i])) || ContainsByte(dot, (byte)(characterarray[i])))
                        {
                            if (ContainsByte(dot, (byte)(characterarray[i])))
                            {
                                Together = Together + characterarray[i].ToString();
                                i++;
                                continue;
                            }
                            Together = Together + characterarray[i].ToString();
                            i++;
                        }
                        i--;
                    }
                    SourceAppart[1][Count] = "\"" + Together + "\"";
                    Count = Count + 1;
                    continue;
                }

                //Nothing
                if (ContainsByte(nothing, One))
                {
                    if ((byte)One != (byte)0x20)
                    {
                        SourceAppart[0][Count] = "\"" + characterarray[i] + "\"";
                        SourceAppart[1][Count] = "\"\"";
                    }
                    else {
                        continue;
                    }
                }
                else
                {
                    //Variables
                    if (CheckByteSize(0x41, (byte)characterarray[i], 0x5A) || CheckByteSize(0x61, (byte)characterarray[i], 0x7A))
                    {
                        string Together = "\"identifier\"";
                        SourceAppart[0][Count] = Together;

                        string NameVariable = "";
                        while (CheckByteSize(0x41, (byte)characterarray[i], 0x5A) || CheckByteSize(0x61, (byte)characterarray[i], 0x7A))
                        {
                            NameVariable = NameVariable + characterarray[i];
                            i++;
                        }
                        i--;
                        SourceAppart[1][Count] = "\"" + NameVariable + "\"";

                        if (CheckBytes(GenerateByteArray(NameVariable),IF)
                            || CheckBytes(GenerateByteArray(NameVariable), WHILE)
                            || CheckBytes(GenerateByteArray(NameVariable), FOR)
                            || CheckBytes(GenerateByteArray(NameVariable), ELSE)
                            || CheckBytes(GenerateByteArray(NameVariable), BREAK)
                            || CheckBytes(GenerateByteArray(NameVariable), SWITCH)
                            || CheckBytes(GenerateByteArray(NameVariable), CONTINUE))
                        {
                            SourceAppart[0][Count] = "\"keyword\"";
                        }
                    }

                    //Strings
                    if ((byte)characterarray[i] == 0x22)
                    {
                        string Together = "\"string\"";
                        SourceAppart[0][Count] = Together;

                        string NameVariable = "";
                        i++;
                        while ((byte)characterarray[i] != 0x22)
                        {
                            NameVariable = NameVariable + characterarray[i];
                            i++;
                        }

                        SourceAppart[1][Count] = "\"" + NameVariable + "\"";
                    }


                    //Seperators
                    if (ContainsByte(separator, One))
                    {
                        SourceAppart[0][Count] = "\"seperator\"";
                        SourceAppart[1][Count] = "\"" + characterarray[i] + "\"";
                    }
                }
                Count = Count + 1;
            }
            SourceAppart[0] = SourceAppart[0].Where(x => !string.IsNullOrEmpty(x)).ToArray();
            SourceAppart[1] = SourceAppart[1].Where(x => !string.IsNullOrEmpty(x)).ToArray();
            return SourceAppart;
        }
    }
}
