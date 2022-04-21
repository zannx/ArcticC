using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static ArcticC.Parser.Parser;
using static ArcticC.Lexer.Lexer;
using static ArcticC.Program;

namespace ArcticC.Evaluator
{
    public static class Extensions
    {
        public static int findIndex<T>(this T[] array, T item)
        {
            return Array.IndexOf(array, item);
        }
    }
    static class ArrayExtensions
    {
        public static void ReplaceAll(this string[] items, string oldValue, string newValue)
        {
            for (int index = 0; index < items.Length; index++)
                if (items[index] == oldValue)
                    items[index] = newValue;
        }
    }
    public class Evaluator
    {
        //Just for a test purpose array
        public static string[][] ProgramArray = { new string[10], new string[10] };
        public static void EvaluatorVoid(string Tree)
        {
            LexeredTable[0].ReplaceAll("\"", "");
            LexeredTable[1].ReplaceAll("\"", "");
            int Count = 0;
            string Action = "";
            int LatestVariableChange = 0;
            bool ZadnjiBool = false;
            for (int i = 0; i <= Tree.Length - 1; i++)
            {
                char[] ProgramArrayChar = Tree.ToCharArray();
                Action = Action + ProgramArrayChar[i];
                Action = Action.Replace("]", "");
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
                        if (JeStevilo || (Action == "string:" || Action == "boolean:"))
                        {
                            ProgramArray[1][Count] = Variable;
                        }
                        else
                        {
                            int KoncnoStevilo = 0;
                            for (int l = 0; l <= Count; l++)
                            {
                                if (ProgramArray[0][Count] == Variable)
                                {
                                    KoncnoStevilo = l;
                                    break;
                                }
                            }
                            ProgramArray[1][Count] = ProgramArray[1][KoncnoStevilo];
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
                                if (JeStevilo == JeStevilo2)
                                {
                                    i = i + 1;
                                    ZadnjiBool = true;
                                }
                                else
                                {
                                    ZadnjiBool = false;
                                    int ZacetekOklepaj = 0;
                                    int KonecOklepaj = 0;
                                    while (true)
                                    {
                                        i = i + 1;
                                        if (ProgramArrayChar[i] == '[')
                                        {
                                            ZacetekOklepaj = ZacetekOklepaj + 1;
                                        }
                                        if (ProgramArrayChar[i] == ']')
                                        {
                                            KonecOklepaj = KonecOklepaj + 1;
                                            if (ZacetekOklepaj == KonecOklepaj)
                                            {
                                                //i = i + 1;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            else {
                                int s;
                                for (s = 0; s <= ProgramArray[1].Length - 1; s++)
                                {
                                    if (ProgramArray[0][s] == Variable1)
                                    {
                                        break;
                                    }
                                    if (JeStevilo && ProgramArray[0][s] == null)
                                    {
                                        ProgramArray[0][s] = Variable1;
                                        ProgramArray[1][s] = Variable1;
                                    }
                                }

                                int s2;
                                for (s2 = 0; s2 <= ProgramArray[1].Length - 1; s2++)
                                {
                                    if (ProgramArray[0][s2] == Variable2)
                                    {
                                        break;
                                    }
                                    if (JeStevilo2 && ProgramArray[0][s2] == null)
                                    {
                                        ProgramArray[0][s2] = Variable2;
                                        ProgramArray[1][s2] = Variable2;
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
                                    int ZacetekOklepaj = 0;
                                    int KonecOklepaj = 0;
                                    while (true)
                                    {
                                        i = i + 1;
                                        if (ProgramArrayChar[i] == '[')
                                        {
                                            ZacetekOklepaj= ZacetekOklepaj+1;
                                        }
                                        if (ProgramArrayChar[i] == ']')
                                        {
                                            KonecOklepaj= KonecOklepaj+1;
                                            if(ZacetekOklepaj==KonecOklepaj)
                                            {
                                                //i = i + 1;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        Action = "";
                        //Count = Count + 1;
                    }
                    if (Action == "drugace$") {
                        if (ZadnjiBool)
                        {
                            while (ProgramArrayChar[i] != ']')
                            {
                                i = i + 1;
                            }
                            ZadnjiBool = false;
                        }
                        else
                        {
                            //while (ProgramArrayChar[i] != '[')
                            //{
                                i = i + 1;
                            //}
                        }
                        Action = "";
                    }
                    if (Action == "izpisi:")
                    {
                        string Variable = "";
                        i++;
                        while (ProgramArrayChar[i] != '$' && i <= Tree.Length - 1)
                        {
                            Variable = Variable + ProgramArrayChar[i];
                            i++;
                        }
                        if (Array.Exists(ProgramArray[0], x => x == Variable))
                        {
                            Console.Write(ProgramArray[1][ProgramArray[0].findIndex(Variable)]);
                        }
                        else {
                            if (Variable.Contains(@"\n"))
                            {
                                Console.WriteLine(Variable.Replace(@"\n",""));
                            }
                            else
                            {
                                Console.Write(Variable);
                            }
                        }
                        Action = "";
                    }
                }
            }
            ProgramArray[0] = ProgramArray[0].Where(x => !string.IsNullOrEmpty(x)).ToArray();
            ProgramArray[1] = ProgramArray[1].Where(x => !string.IsNullOrEmpty(x)).ToArray();
            //for (int i = 0; i <= ProgramArray[0].Length - 1; i++)
            //{
            //    Console.WriteLine(ProgramArray[0][i] + " >>> " + ProgramArray[1][i]);
            //}
        }
    }
}
