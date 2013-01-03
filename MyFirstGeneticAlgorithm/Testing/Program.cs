using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            UnitTest1.TestMethod1();
        }
    }

    public static class UnitTest1
    {
        public static void TestMethod1()
        {
            string decodedStr = "22+-72";
            StringBuilder decoded = new StringBuilder(decodedStr);

            Type needed = GetType(decoded[0]) == Type.Number ? Type.Operator : Type.Number;
            for (int i = 1; i < decoded.Length; i++)
            {
                if (GetType(decoded[i]) != needed)
                {
                    decoded.Remove(i, 1);
                }
                else
                    needed = GetType(decoded[i]) == Type.Number ? Type.Operator : Type.Number;
            }

            Console.WriteLine(decoded.ToString() == "2+7");
            Console.ReadKey(true);
        }

        enum Type
        {
            Operator,
            Number
        }
        static Type GetType(char character)
        {
            List<char> operators = new List<char> { '+', '-', '*', '/' };

            return operators.Contains(character) ? Type.Operator : Type.Number;
        }
    }
}
