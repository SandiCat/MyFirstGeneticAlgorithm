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
        static Dictionary<string, char> foo = new Dictionary<string, char> //Chromosomes will be decoded using this dictionary
        {
            {"0000", '0'},
            {"0001", '1'},
            {"0010", '2'},
            {"0011", '3'},
            {"0100", '4'},
            {"0101", '5'},
            {"0110", '6'},
            {"0111", '7'},
            {"1000", '8'},
            {"1001", '9'},
            {"1010", '+'},
            {"1011", '-'},
            {"1100", '*'},
            {"1101", '/'}
        };

        public static void TestMethod1()
        {
            Console.WriteLine(FitnessScore("011010100101110001001101001010100001", 42));
            Console.ReadKey(true);
        }

        static float FitnessScore(string chromosome, int wantedValue)
        {
            //First decode the chromosome:
            string decoded = "";

            for (int i = 0; i < chromosome.Length; i += 4)
            {
                string key = chromosome.Substring(i, 4);
                char character;
                try { character = foo[key]; decoded += character; }
                catch (KeyNotFoundException) { }
            }

            //Remove meaningless genes:
            string premuted = "";
            if (!IsOperator(decoded[0])) { premuted += "+"; }
            premuted += decoded[0];

            bool operatorNeeded = !IsOperator(decoded[0]);

            for (int i = 1; i < decoded.Length; i++)
            {
                if (operatorNeeded)
                {
                    if (IsOperator(decoded[i]))
                    {
                        premuted += decoded[i];
                        operatorNeeded = !operatorNeeded;
                    }
                }
                else
                {
                    if (!IsOperator(decoded[i]))
                    {
                        premuted += decoded[i];
                        operatorNeeded = !operatorNeeded;
                    }
                }
            }

            //Calculate the value of the encoded expression
            int value = 0;

            for (int i = 1; i < premuted.Length; i += 2) //go through every second character (every digit)
            {
                int digit = int.Parse(premuted[i].ToString());
                char operation = premuted[i - 1];

                switch (operation)
                {
                    case '+':
                        value += digit;
                        break;
                    case '-':
                        value -= digit;
                        break;
                    case '*':
                        value *= digit;
                        break;
                    case '/':
                        value /= digit;
                        break;
                }
            }

            //And finally, calculate the fitness score:
            float score = 0;
            try { score = 1f / Math.Abs(wantedValue - value); }
            catch (DivideByZeroException) { Console.WriteLine("Houston, we have a winner!!!"); };

            return score;
        }

        static bool IsOperator(char character)
        {
            List<char> operators = new List<char> { '+', '-', '*', '/' };
            return operators.Contains(character) ? true : false;
        }
    }
}
