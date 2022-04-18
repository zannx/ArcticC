using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static ArcticC.Parser.Parser;
using static ArcticC.Lexer.Lexer;
using static ArcticC.Program;

namespace ArcticC.Evaluator
{
    public class Evaluator
    {
        public static string[][] ProgramArray = { new string[10], new string[10] };
        public static void EvaluatorVoid(string Tree)
        {
            //Just for a test purpose array
        

            int Count = 0;
            string Action = "";
            int LatestVariableChange = 0;
            bool ZadnjiBool;
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
                        Count = Count + 1;
                        ProgramArray[0][Count] = Variable;
                        LatestVariableChange = Count;
                        Action = "";
                    }
                    if (Action == "integer:" || Action == "string:"
                        || Action == "decimal:" || Action == "boolean:" || Action == "identifier:")
                    {
                        string Variable = "";
                        i++;
                        while (ProgramArrayChar[i] != '$' && i <= Tree.Length - 1)
                        {
                            Variable = Variable + ProgramArrayChar[i];
                            i++;
                        }
                        var JeStevilo = decimal.TryParse(Variable, out _);
                        if (JeStevilo)
                        {
                            ProgramArray[1][Count] = Variable;
                        }
                        else
                        {
                            int KocnoStevilo = 0;
                            for (int l = 0; l <= Count; l++)
                            {
                                if (ProgramArray[0][Count] == Variable)
                                {
                                    KocnoStevilo = l;
                                    break;
                                }
                            }
                            ProgramArray[1][Count] = ProgramArray[1][KocnoStevilo];
                        }
                        Action = "";
                        //Count = Count + 1;
                    }
                    if (Action == "plus$")
                    {
                        string Variable = "";
                        i++;
                        while (ProgramArrayChar[i] != '$' && i <= Tree.Length - 1)
                        {
                            if (Variable == "integer:" || Variable == "decimal:" || Variable == "identifier:")
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
                        //Count = Count + 1;
                    }
                    if (Action == "ce$")
                    {
                        string Variable1 = "";
                        i++;
                        while (ProgramArrayChar[i] != '$' && i <= Tree.Length - 1)
                        {
                            Variable1 = Variable1 + ProgramArrayChar[i];
                            i++;
                        }

                        string Operacija = "";
                        i++;
                        while (ProgramArrayChar[i] != '$' && i <= Tree.Length - 1)
                        {
                            Operacija = Operacija + ProgramArrayChar[i];
                            i++;
                        }

                        string Variable2 = "";
                        i++;
                        while (ProgramArrayChar[i] != '$' && i <= Tree.Length - 1)
                        {
                            Variable2 = Variable2 + ProgramArrayChar[i];
                            i++;
                        }

                        var JeStevilo = decimal.TryParse(Variable1, out _);
                        var JeStevilo2 = decimal.TryParse(Variable2, out _);

                        if (Operacija == "equalsequals")
                        {
                            if (JeStevilo && JeStevilo2)
                            {

                            }
                            else {
                                int s;
                                for (s = 0; s <= ProgramArray[1].Length - 1; s++)
                                {
                                    if (ProgramArray[0][s] == Variable1)
                                    {
                                        break;
                                    }
                                }

                                int s2;
                                for (s2 = 0; s2 <= ProgramArray[1].Length - 1; s2++)
                                {
                                    if (ProgramArray[0][s2] == Variable2)
                                    {
                                        break;
                                    }
                                }

                                if (ProgramArray[1][s] == ProgramArray[1][s2])
                                {
                                    //i = i + 5;
                                    i++;
                                    ZadnjiBool = true;
                                }
                                else
                                {
                                    ZadnjiBool = false;
                                    while (ProgramArrayChar[i] != ']')
                                    {
                                        i = i + 1;
                                    }
                                }
                            }
                        }
                        Action = "";
                        //Count = Count + 1;
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
