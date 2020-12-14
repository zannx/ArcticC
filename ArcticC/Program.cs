using System;
using static ArcticC.StringCompiler.CompilerStr;
using static ArcticC.StringCompiler.ByteArrays;

namespace ArcticC
{

    class Program
    {
   
        static void Main(string[] args)
        {
            //Basic variables
            string SourceMain = "";
            string Assembly = "";

            while (true)
            { 
                string Input = Convert.ToString(Console.ReadLine());
                
                if (CheckBytes(CompilerString(Input.ToLower()), compile))
                {
                    char[] SourceMainChar = SourceMain.ToCharArray();

                    string Object = "";
                    for (int i = 0; i <= SourceMainChar.Length - 1; i++)
                    {
                    
                        if (SourceMainChar[i] == 0x20)
                        {
                            
                            //End
                            if (CheckBytes(CompilerString(Object.ToLower()), end))
                            {
                                Console.WriteLine(Object.ToLower());
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
