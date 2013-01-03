using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstGeneticAlgorithm
{
    class Program
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

        static void Main(string[] args)
        {
            //Constant varables (parameters):
                const float crossoverRate = 0.7f;
                const decimal mutationRate = 0.001M;
                const int chromosomeLenght = 28;
                const int amount = 50;

            //Program specific values:
                List<string> currentChromosomes = new List<string>();
                Random random = new Random(1337);
                const int wantedValue = 42;

            //Generate [amount] chromosomoes and fill the list with them (actually its just generating random binary strings):
                for (int i = 0; i < amount; i++)
                {
                    string chromosome = "";

                    for (int j = 0; j < chromosomeLenght; j++)
                    {
                        chromosome += random.Next(2).ToString(); //This will add either 1 or 0
                    }

                    currentChromosomes.Add(chromosome);
                }

            //Evolve populations until a solution is found:
                bool solutionFound = false;
                while (!solutionFound)
                {
                    List<string> newChromosomes = new List<string>();

                    //Generate new chromosones by doing crossovers and mutations and add them to newChromosones:
                        while (newChromosomes.Count != amount)
                        {
                            
                        }
                }
        }

        //static int FitnessScore(string chromosome, int wantedValue)
        //{
        //    //First decode the chromosome:
        //    string decoded = "";   

        //    for (int i = 0; i < chromosome.Length; i += 4)
        //    {
        //        string key = chromosome.Substring(i, 4);
        //        char character;
        //        try { character = foo[key]; decoded += character; }
        //        catch (KeyNotFoundException) { }
        //    }

        //    //Remove meaningless genes:
        //    Type needed = GetType(decoded[0]) == Type.Number ? Type.Operator : Type.Number;
        //    for (int i = 1; i < decoded.Length; i++)
        //    {
        //        if (GetType(decoded[i]) != needed)
        //        {
        //            decoded.Remove(i);
        //            i--;
        //        }

        //        needed = GetType(decoded[i]) == Type.Number ? Type.Operator : Type.Number;
        //    }
        //}

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
