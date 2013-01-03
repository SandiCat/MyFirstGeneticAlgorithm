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
            CalculateTheSolution();
        }

        static void CalculateTheSolution()
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
                    //To generate new chromosomes i will select two mebers of the current group with the rulette wheel method
                    //and then do crosover to get two new chromosomes. Then i'll mutate the new chromosomes and add them to the list.

                    //Select two chromosomes:

                    var sorted = (from s in currentChromosomes
		                orderby FitnessScore(s, wantedValue)
		                select s).ToList();

                    string[] chromosome = new string[1];

                    float upper = 0;
                    foreach (var chromo in sorted) upper += FitnessScore(chromo, wantedValue);
                    
                    for (int i = 0; i <= upper; i++) //do this two times, once per chromosome
                    {
                        float rand = (float)random.NextDouble(); //where the "ball" lands
                        float value = 0;
                        
                        int chosenChromosome;
                        for (chosenChromosome = 0; value < rand; chosenChromosome++)
                        {
                            value += FitnessScore(sorted[chosenChromosome], wantedValue);
                        }
                        chosenChromosome--;

                        chromosome[i] = sorted[chosenChromosome];
                    }

                    //Do crosover:
                    if (random.NextDouble() > crossoverRate)
                    {
                        int point = random.Next(0, chromosomeLenght);

                    }
                }
            }
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
