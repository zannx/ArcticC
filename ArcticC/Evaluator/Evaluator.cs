using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static ArcticC.Parser.Parser;
using static ArcticC.Lexer.Lexer;

namespace ArcticC.Evaluator
{
    public class Evaluator
    {
        public static void EvaluatorVoid(string Tree)
        {
            //Just for a test purpose array
            string[][] ProgramArray = {new string [10], new string[10]};

            int Count = 0;
            string Action = "";
            int LatestVariableChange = 0;
            for (int i = 0; i <= Tree.Length - 1; i++)
            {
                char[] ProgramArrayChar = Tree.ToCharArray();
                Action = Action + ProgramArrayChar[i];
                if (ProgramArrayChar[i] == '$' || ProgramArrayChar[i] == ':')
                {
                    if (Action == "assign$")
                    {
                        string Variable = "";
                        i++;
                        while (ProgramArrayChar[i] != '$' && i <= Tree.Length - 1)
                        {
                            Variable = Variable + ProgramArrayChar[i];
                            i++;
                        }
                        for (int Counter = 0; Counter <= Count; Counter++)
                        {
                            if (ProgramArray[0][Counter] == Variable)
                            {
                                ProgramArray[0][Counter] = null;
                                ProgramArray[1][Counter] = null;
                            }
                        }
                        ProgramArray[0][Count] = Variable;
                        LatestVariableChange = Count;
                        Action = "";
                    }
                    if (Action == "integer:" || Action == "string:"
                        || Action == "decimal:" || Action == "boolean:")
                    {
                        string Variable = "";
                        i++;
                        while (ProgramArrayChar[i] != '$' && i <= Tree.Length - 1)
                        {
                            Variable = Variable + ProgramArrayChar[i];
                            i++;
                        }
                        ProgramArray[1][Count] = Variable;
                        Action = "";
                        Count = Count + 1;
                    }
                    if (Action == "plus$")
                    {
                        string Variable = "";
                        i++;
                        while (ProgramArrayChar[i] != '$' && i <= Tree.Length - 1)
                        {
                            if (Variable == "integer:" || Variable == "decimal:")
                            {
                                string Number = "";
                                while (ProgramArrayChar[i] != '$' && i <= Tree.Length - 1)
                                {
                                    Number = Number + ProgramArrayChar[i];
                                    i++;
                                }
                                decimal PretvojenoStevilo;
                                Decimal.TryParse(ProgramArray[1][LatestVariableChange], out PretvojenoStevilo);
                                ProgramArray[1][LatestVariableChange] = Convert.ToString(PretvojenoStevilo + Convert.ToDecimal(Number));
                                break;
                            }
                            Variable = Variable + ProgramArrayChar[i];
                            i++;
                        }
                        Action = "";
                        Count = Count + 1;
                    }
                }
            }
            ProgramArray[0] = ProgramArray[0].Where(x => !string.IsNullOrEmpty(x)).ToArray();
            ProgramArray[1] = ProgramArray[1].Where(x => !string.IsNullOrEmpty(x)).ToArray();
            for (int i = 0; i <= ProgramArray[0].Length - 1; i++)
            {
                Console.WriteLine(ProgramArray[0][i] + " >>> " + ProgramArray[1][i]);
            }
        }
    }
}
