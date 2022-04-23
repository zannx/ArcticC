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
                if (LexeredArray[1][i].Replace("\"", string.Empty).Trim() == "}") {
                    Tree = Tree + "]";
                }
                if (LexeredArray[1][i].Replace("\"", string.Empty).Trim() == "{")
                {
                    Tree = Tree + "[";
                }
                if (LexeredArray[1][i].Replace("\"", string.Empty).Trim() == "izpisi") {
                    Tree = Tree + "izpisi:" + LexeredArray[1][i + 2].Replace("\"", string.Empty).Trim() + "$";
                }
            }
            return Tree;
        }
    }
}
