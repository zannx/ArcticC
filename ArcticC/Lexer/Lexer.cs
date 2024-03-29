﻿using System;
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
            string[][] SourceAppart = new string[][] { new string[characterarray.Length], new string[characterarray.Length] };

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
                        if (Together == "=" && characterarray[i].ToString() == "+" || Together == "=" && characterarray[i].ToString() == "-")
                        {
                            i--;
                            //continue;
                        }
                        else {
                            Together = Together + characterarray[i].ToString();
                            SourceAppart[1][Count] = "\"" + Together + "\"";
                        }
                    }
                    Count = Count + 1;
                    continue;
                }

                //Integer or decimal
                if (ContainsByte(integers, One))
                {
                    string Together = characterarray[i].ToString();
                    SourceAppart[0][Count] = "\"integer\"";

                    if (ContainsByte(integers, (byte)characterarray[i + 1]) || 0x2E == (byte)characterarray[i + 1])
                    {
                        i++;
                        while (ContainsByte(integers, (byte)(characterarray[i])) || 0x2E == (byte)(characterarray[i]))
                        {
                            if (0x2E == (byte)(characterarray[i]))
                            {
                                Together = Together + characterarray[i].ToString();
                                SourceAppart[0][Count] = "\"decimal\"";
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
                    else
                    {
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

                        //Keywords
                        if (CheckBytes(GenerateByteArray(NameVariable), IF)
                            || CheckBytes(GenerateByteArray(NameVariable), WHILE)
                            || CheckBytes(GenerateByteArray(NameVariable), FOR)
                            || CheckBytes(GenerateByteArray(NameVariable), ELSE)
                            || CheckBytes(GenerateByteArray(NameVariable), BREAK)
                            || CheckBytes(GenerateByteArray(NameVariable), SWITCH)
                            || CheckBytes(GenerateByteArray(NameVariable), CONTINUE)
                            || CheckBytes(GenerateByteArray(NameVariable), RETURN)
                            || CheckBytes(GenerateByteArray(NameVariable), FUNC))
                        {
                            SourceAppart[0][Count] = "\"keyword\"";
                        }

                        //Booleans
                        if (CheckBytes(GenerateByteArray(NameVariable), TRUE)
                            || CheckBytes(GenerateByteArray(NameVariable), FALSE))
                        {
                            SourceAppart[0][Count] = "\"boolean\"";
                            SourceAppart[1][Count] = "\"" + NameVariable + "\"";
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

                    //Character
                    if ((byte)characterarray[i] == 0x27)
                    {
                        string Together = "\"char\"";
                        SourceAppart[0][Count] = Together;

                        string NameVariable = "";
                        i++;
                        while ((byte)characterarray[i] != 0x27)
                        {
                            NameVariable = NameVariable + characterarray[i];
                            i++;
                        }

                        if (NameVariable.Length > 1)
                        {
                            Console.WriteLine("Error: you haven't declared a character but a string in character type at: {0}", NameVariable);
                            break;
                        }

                        SourceAppart[1][Count] = "\"" + NameVariable + "\"";
                    }

                    //Comment
                    if ((byte)characterarray[i] == 0xB0)
                    {
                        string Together = "\"comment\"";
                        SourceAppart[0][Count] = Together;

                        string NameVariable = "";
                        i++;
                        while ((byte)characterarray[i] != 0xB0)
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
