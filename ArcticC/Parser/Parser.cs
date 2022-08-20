using System;
using System.Collections.Generic;
using System.Text;
using static ArcticC.Lexer.LexerFunctions;
using static ArcticC.Evaluator.Evaluator;
using static ArcticC.StringCompiler.CompilerStr;

namespace ArcticC.Parser
{
    public class Parser
    {
        public static string ParserCheck(string[][] LexeredArray)
        {
            string Tree = "";
            for (int i = 0; i <= LexeredArray[0].Length - 1; i++)
            {
                string Variable = "";
                if (ContainsByte(GenerateByteArray(LexeredArray[1][i].Replace("\"", string.Empty).Trim()), (byte)0x3D))
                {
                    Variable = LexeredArray[1][i - 1].Replace("\"", string.Empty).Trim();
                    Tree = Tree + "assign$";
                    Tree = Tree + string.Concat(Variable, "$");
                    string Tokens = "";
                    i++;
                    while (LexeredArray[1][i].Replace("\"", string.Empty).Trim() != ";")
                    {
                        Tokens = Tokens + LexeredArray[1][i].Replace("\"", string.Empty).Trim();
                        if (LexeredArray[0][i].Replace("\"", string.Empty).Trim() == "integer" || LexeredArray[0][i].Replace("\"", string.Empty).Trim() == "string"
                        || LexeredArray[0][i].Replace("\"", string.Empty).Trim() == "decimal" || LexeredArray[0][i].Replace("\"", string.Empty).Trim() == "boolean"
                        || LexeredArray[0][i].Replace("\"", string.Empty).Trim() == "identifier")
                        {
                          Tree = Tree + LexeredArray[0][i].Replace("\"", string.Empty).Trim() + ":" + LexeredArray[1][i].Replace("\"", string.Empty).Trim() + "$";
                        }
                        if (LexeredArray[0][i].Replace("\"", string.Empty).Trim() == "operator")
                        {
                            if (LexeredArray[1][i].Replace("\"", string.Empty).Trim() == "+")
                            {
                                Tree = Tree + "plus" + "$";
                            }
                            if (LexeredArray[1][i].Replace("\"", string.Empty).Trim() == "-")
                            {
                                Tree = Tree + "minus" + "$";
                            }
                        }
                        i++;
                    }
                }
                if (LexeredArray[1][i].Replace("\"", string.Empty).Trim() == "if")
                {
                    if (LexeredArray[1][i + 3].Replace("\"", string.Empty).Trim() == "==")
                    {
                        Tree = Tree + "ce" + "$" + LexeredArray[1][i + 2].Replace("\"", string.Empty).Trim() + "$" + "equalsequals" + "$" + LexeredArray[1][i + 4].Replace("\"", string.Empty).Trim() + "$";
                        i = i + 5;
                    }
                }
                if (LexeredArray[1][i].Replace("\"", string.Empty).Trim() == "else")
                {
                    Tree = Tree + "drugace" + "$";
                }

                //FUNCTIONS
                if (LexeredArray[1][i].Replace("\"", string.Empty).Trim() == "func")
                {
                    Tree = Tree + "func" + "$" + LexeredArray[1][i + 1].Replace("\"", string.Empty).Trim() + "$";
                    int Count = 3;
                    while (LexeredArray[1][i + Count].Replace("\"", string.Empty).Trim() != ")")
                    {
                        Tree = Tree + LexeredArray[1][i + Count].Replace("\"", string.Empty).Trim() + "$";
                        Count++;
                    }
                }
                if (LexeredArray[0][i].Replace("\"", string.Empty).Trim() == "identifier" && LexeredArray[1][i].Replace("\"", string.Empty).Trim() != "izpisi") {
                    int CountOne = 2;
                    while (LexeredArray[1][i + CountOne].Replace("\"", string.Empty).Trim() != ")")
                    {
                        //Console.WriteLine(LexeredArray[1][i + CountOne].Replace("\"", string.Empty).Trim());
                        CountOne++;
                        if ((i + CountOne) > LexeredArray[0].Length - 1)
                        {
                            break;
                        }
                    }
                    if ((i + CountOne) > LexeredArray[0].Length - 1)
                    {
                        break;
                    }
                    if (LexeredArray[1][i + CountOne + 1].Replace("\"", string.Empty).Trim() == ";")
                    {

                        if (LexeredArray[0][i + 1].Replace("\"", string.Empty).Trim() == "seperator" && LexeredArray[1][i + 1].Replace("\"", string.Empty).Trim() == "(" && LexeredArray[1][i].Replace("\"", string.Empty).Trim() != "izpisi")
                        {
                            Tree = Tree + "func_call" + "$" + LexeredArray[1][i].Replace("\"", string.Empty).Trim() + "$";
                            int Count = 2;
                            while (LexeredArray[1][i + Count].Replace("\"", string.Empty).Trim() != ")")
                            {
                                Tree = Tree + LexeredArray[1][i + Count].Replace("\"", string.Empty).Trim() + "$";
                                Count++;
                            }
                        }
                    }
                }

                //BRACKETS
                if (LexeredArray[1][i].Replace("\"", string.Empty).Trim() == "}") {
                    Tree = Tree + "]";
                }
                if (LexeredArray[1][i].Replace("\"", string.Empty).Trim() == "{")
                {
                    Tree = Tree + "[";
                }
                if (LexeredArray[1][i].Replace("\"", string.Empty).Trim() == "izpisi") {
                    Tree = Tree + "izpisi:" + LexeredArray[1][i + 2].Replace("\"", string.Empty) + "$";
                }
            }
            return Tree;
        }
    }
}
