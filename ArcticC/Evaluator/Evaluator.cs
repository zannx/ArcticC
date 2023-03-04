using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static ArcticC.Parser.Parser;
using static ArcticC.Lexer.Lexer;
using static ArcticC.Program;
using System.Threading;
using System.Globalization;

namespace ArcticC.Evaluator
{
    public static class Extensions
    {
        public static int findIndex<T>(this T[] array, T item)
        {
            return Array.IndexOf(array, item);
        }
    }
    public class Evaluator
    {
        //Just for a test purpose array
        public static string[][] ProgramArray = { new string[100], new string[100] };
        public static string[][] FunctionsArray = { new string[100], new string[100] };
        public static void EvaluatorVoid(string Tree)
        {
            int Count = -1;
            int FunctionsCount = 0;
            int FunctionsCountend = 0;
            string Action = "";
            int LatestVariableChange = 0;
            int ProgramStayed = 0;
            bool ZadnjiBool = false;
            string FunctionNamePublic = "";
            Stack<int> x = new Stack<int>();

            for (int i = 0; i <= Tree.Length - 1; i++)
            {
                char[] ProgramArrayChar = Tree.ToCharArray();
                Action = Action + ProgramArrayChar[i];
                Action = Action.Replace("]", "");
                Action = Action.Replace("[", "");
               
                if (FunctionNamePublic != "" && i == Convert.ToInt32(FunctionsArray[1][FunctionsArray[0].findIndex(FunctionNamePublic + "end")]))
                {
                    i = x.Pop();
                }
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
                        for (int Counter = 0; Counter <= ProgramArray[0].Length-1; Counter++)
                        {
                            if (ProgramArray[0][Counter] == Variable)
                            {
                                ProgramArray[0][Counter] = null;
                                //ProgramArray[1][Counter] = null;
                            }
                        }
                        Count = ProgramArray[0].findIndex(null);
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
                            for (int l = 0; l <= ProgramArray[0].Length-1; l++)
                            {
                                if (ProgramArray[0][l] == Variable)
                                {
                                    KoncnoStevilo = l;
                                    break;
                                }
                            }
                            ProgramArray[1][Count] = ProgramArray[1][KoncnoStevilo]; //Tule ga spremeni
                        }
                        Action = "";
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
                                Decimal.TryParse(ProgramArray[1][LatestVariableChange], System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out PretvojenoStevilo);
    

                                decimal PretvojenoSteviloDve;

                                if (Decimal.TryParse(Number, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out PretvojenoSteviloDve))
                                {
                                    ProgramArray[1][LatestVariableChange] = Convert.ToString(Math.Round(PretvojenoStevilo + Convert.ToDecimal(Number, CultureInfo.InvariantCulture),10), CultureInfo.InvariantCulture);
                                }
                                else {
                                    string SteviloVArrayu = ProgramArray[1][ProgramArray[0].findIndex(Number)];
                                    ProgramArray[1][LatestVariableChange] = Convert.ToString(Math.Round(PretvojenoStevilo + Convert.ToDecimal(SteviloVArrayu, CultureInfo.InvariantCulture),10), CultureInfo.InvariantCulture);
                                }
                                
                                break;
                            }
                            Variable = Variable + ProgramArrayChar[i];
                            i++;
                        }
                        Action = "";
                    }
                    if (Action == "minus$")
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
                                Decimal.TryParse(ProgramArray[1][LatestVariableChange], System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out PretvojenoStevilo);

                                decimal PretvojenoSteviloDve;

                                if (Decimal.TryParse(Number, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out PretvojenoSteviloDve))
                                {
                                    ProgramArray[1][LatestVariableChange] = Convert.ToString(Math.Round(PretvojenoStevilo - Convert.ToDecimal(Number, CultureInfo.InvariantCulture),10), CultureInfo.InvariantCulture);
                                }
                                else
                                {
                                    string SteviloVArrayu = ProgramArray[1][ProgramArray[0].findIndex(Number)];
                                    ProgramArray[1][LatestVariableChange] = Convert.ToString(Math.Round(PretvojenoStevilo - Convert.ToDecimal(SteviloVArrayu, CultureInfo.InvariantCulture),10), CultureInfo.InvariantCulture);
                                }

                                break;
                            }
                            Variable = Variable + ProgramArrayChar[i];
                            i++;
                        }
                        Action = "";
                    }
                    if (Action == "multiply$")
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
                                Decimal.TryParse(ProgramArray[1][LatestVariableChange], System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out PretvojenoStevilo);

                                decimal PretvojenoSteviloDve;

                                if (Decimal.TryParse(Number, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out PretvojenoSteviloDve))
                                {
                                    if (PretvojenoStevilo * Convert.ToDecimal(Number, CultureInfo.InvariantCulture) > decimal.MaxValue) { } else
                                    {
                                        ProgramArray[1][LatestVariableChange] = Convert.ToString(Math.Round(PretvojenoStevilo * Convert.ToDecimal(Number, CultureInfo.InvariantCulture),10), CultureInfo.InvariantCulture);
                                    }  
                                }
                                else
                                {
                                    string SteviloVArrayu = ProgramArray[1][ProgramArray[0].findIndex(Number)];
                                    if (PretvojenoStevilo * Convert.ToDecimal(SteviloVArrayu, CultureInfo.InvariantCulture) > decimal.MaxValue) { } else { 
                                        ProgramArray[1][LatestVariableChange] = Convert.ToString(Math.Round(PretvojenoStevilo * Convert.ToDecimal(SteviloVArrayu, CultureInfo.InvariantCulture),10), CultureInfo.InvariantCulture);
                                    }
                                }
                                break;
                            }
                            Variable = Variable + ProgramArrayChar[i];
                            i++;
                        }
                        Action = "";
                    }
                    if (Action == "divide$")
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
                                Decimal.TryParse(ProgramArray[1][LatestVariableChange], System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out PretvojenoStevilo);

                                decimal PretvojenoSteviloDve;

                                if (Decimal.TryParse(Number, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out PretvojenoSteviloDve))
                                {
                                    ProgramArray[1][LatestVariableChange] = Convert.ToString(Math.Round(PretvojenoStevilo / Convert.ToDecimal(Number, CultureInfo.InvariantCulture),10), CultureInfo.InvariantCulture);
                                }
                                else
                                {
                                    string SteviloVArrayu = ProgramArray[1][ProgramArray[0].findIndex(Number)];
                                    ProgramArray[1][LatestVariableChange] = Convert.ToString(Math.Round(PretvojenoStevilo / Convert.ToDecimal(SteviloVArrayu, CultureInfo.InvariantCulture),10), CultureInfo.InvariantCulture);
                                }

                                break;
                            }
                            Variable = Variable + ProgramArrayChar[i];
                            i++;
                        }
                        Action = "";
                    }
                    if (Action == "func$")
                    {
                        string FunctionName = "";
                        i++;
                        while (ProgramArrayChar[i] != '$' && i <= Tree.Length - 1)
                        {
                            FunctionName = FunctionName + ProgramArrayChar[i];
                            i++;
                        }
                        bool exists = Array.Exists(FunctionsArray[0], el => el == FunctionName); 
                        if (exists==false)
                        {
                            FunctionsArray[0][FunctionsCount] = FunctionName;
                            //FunctionsCount++;
                            int ZacetekOklepaj = 0;
                            int KonecOklepaj = 0;
                            bool FirstTime = false;
                            while (true)
                            {
                                i = i + 1;
                                if (i > Tree.Length - 1)
                                {
                                    break;
                                }
                                if (ProgramArrayChar[i] == '[')
                                {
                                    if (!FirstTime)
                                    {
                                        FunctionsArray[1][FunctionsCount] = i.ToString();
                                        ZacetekOklepaj = ZacetekOklepaj + 1;
                                        FirstTime = true;
                                    }
                                    else {
                                        ZacetekOklepaj = ZacetekOklepaj + 1;
                                    }
                                }
                                if (ProgramArrayChar[i] == ']')
                                {
                                    KonecOklepaj = KonecOklepaj + 1;
                                    if (ZacetekOklepaj == KonecOklepaj)
                                    {
                                        FunctionsCountend = FunctionsCount + 1;
                                        FunctionsArray[0][FunctionsCountend] = FunctionName+"end";
                                        FunctionsArray[1][FunctionsCountend] = Convert.ToString(i);
                                        break;
                                    }
                                }
                            }
                            FunctionsCount = FunctionsCountend;
                            FunctionsCount++;
                        }
                        Action = "";
                    }
                    if (Action == "func_call$")
                    {
                        FunctionNamePublic = "";
                        i++;
                        while (ProgramArrayChar[i] != '$' && i <= Tree.Length - 1)
                        {
                            FunctionNamePublic = FunctionNamePublic + ProgramArrayChar[i];
                            i++;
                        }
                        ProgramStayed = i;

                        x.Push(i); //Magic

                        int Position = 0;

                        int Counter = 0;
                        while (true)
                        {
                            string NameVariable = "";
                            if (ProgramArrayChar[Counter] == '$')
                            {
                                Counter++;
                            }
                            while (ProgramArrayChar[Counter] != '$' && i <= Tree.Length - 1)
                            {
                                NameVariable = NameVariable + ProgramArrayChar[Counter];
                                Counter++;
                            }
                            if (NameVariable == FunctionNamePublic)
                            {
                                Position = Counter;
                                break;
                            }
                        }

                        while (true)
                        {
                            string NameVariable = "";
                            if (ProgramArrayChar[Position] == '$')
                            {
                                Position++;
                                if (Position == ProgramArrayChar.Length)
                                {
                                    break;
                                }
                            }
                            while (ProgramArrayChar[Position] != '$' && i <= Tree.Length - 1)
                            {
                                if (ProgramArrayChar[Position] == '[')
                                {
                                    break;
                                }
                                NameVariable = NameVariable + ProgramArrayChar[Position];
                                Position++;
                            }

                            if (ProgramArrayChar[Position] == '[')
                            {
                                i = Convert.ToInt32(FunctionsArray[1][FunctionsArray[0].findIndex(FunctionNamePublic)]);
                                break;
                            }

                            string Value = "";
                            i++;
                            if (i == Tree.Length)
                            {
                                i = Convert.ToInt32(FunctionsArray[1][FunctionsArray[0].findIndex(FunctionNamePublic)]);
                                //InFunction = true;
                                break;
                            }
                            while (ProgramArrayChar[i] != '$' && i <= Tree.Length - 1)
                            {
                                Value = Value + ProgramArrayChar[i];
                                i++;
                            }

                            for (int CounterD = 0; CounterD <= ProgramArray[0].Length - 1; CounterD++)
                            {
                                if (ProgramArray[0][CounterD] == null)
                                {
                                    ProgramArray[0][CounterD] = NameVariable;
                                    if (Array.Exists(ProgramArray[0], el => el == Value))
                                    {
                                        ProgramArray[1][CounterD] = ProgramArray[1][ProgramArray[0].findIndex(Value)]; 
                                    } else
                                    {
                                        ProgramArray[1][CounterD] = Value;
                                    }
                                    break;
                                }
                            }

                            var JeStevilo = decimal.TryParse(Value, out _);
                            if (!JeStevilo)
                            {
                                i = Convert.ToInt32(FunctionsArray[1][FunctionsArray[0].findIndex(FunctionNamePublic)]);
                                break;
                            }
                        }
                        Action = "";
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
                                    if (ProgramArray[0][s2] == Variable2 || ProgramArray[1][s2] == Variable2)
                                    {
                                        break;
                                    }
                                    if (JeStevilo2 && ProgramArray[0][s2] == null)
                                    {
                                        ProgramArray[0][s2] = Variable2;
                                        ProgramArray[1][s2] = Variable2;
                                        break;
                                    }
                                    else {
                                        if (Variable2 == "false" || Variable2 == "true")
                                        {
                                            s2 = ProgramArray[0].findIndex(null);
                                            ProgramArray[0][s2] = Variable2;
                                            ProgramArray[1][s2] = Variable2;
                                            break;
                                        }
                                    }
                                }

                                //if (s <= ProgramArray.Length - 1 && s2 <= ProgramArray.Length - 1) {
                                    if (ProgramArray[1][s] == ProgramArray[1][s2])
                                    {
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
                                                    break;
                                                }
                                            }
                                        }
                                    } 
                                }
                                
                          //  }
                        }
                        Action = "";
                    }
                    if (Action == "drugace$") {
                        if (ZadnjiBool)
                        {
                            while (ProgramArrayChar[i] != ']')
                            {
                                i = i + 1;
                            }
                            while (ProgramArrayChar[i] == ']')
                            {
                                if (i == Convert.ToInt32(FunctionsArray[1][FunctionsArray[0].findIndex(FunctionNamePublic + "end")])-1)
                                {
                                    break;
                                }
                                i = i + 1;
                            }
                            ZadnjiBool = false;
                        }
                        else
                        {
                           i = i + 1;
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
                if (Action.Contains("$"))
                {
                    //Console.WriteLine(Action + " " + i.ToString());
                    Action = "";
                }
            }
            ProgramArray[0] = ProgramArray[0].Where(x => !string.IsNullOrEmpty(x)).ToArray();
            ProgramArray[1] = ProgramArray[1].Where(x => !string.IsNullOrEmpty(x)).ToArray();
        }
    }
}
