using System;
using static ArticCSharp.StringCompiler.CompilerStr;

namespace ArticCSharp
{

    class Program
    {
   
        static void Main(string[] args)
        {

            string SourceMain = "";
            string Assembly = "";

            while (true)
            { 
                string Input = Convert.ToString(Console.ReadLine());
                
                if (Input.ToLower() == "compile")
                {
                    char[] SourceMainChar = SourceMain.ToCharArray();

                    string Object = "";
                    for (int i = 0; i <= SourceMainChar.Length - 1; i++)
                    {
                    
                        if (SourceMainChar[i] == 0x20)
                        {
                            
                            //End
                            if (CompilerString(Object.ToLower()) == "656E64")
                            {
                                Console.WriteLine(CompilerString(Object.ToLower()));
                            }

                            //Check string what it is and convert it to C#
                            Console.WriteLine(Object.ToString());

                            Object = "";
                            continue;
                        }
                        Object = Object + SourceMainChar[i].ToString();
                    }

                    //Run C# code and return result

                }
                else
                {
                    SourceMain = Input;
                }
            }
        }
    }
}
