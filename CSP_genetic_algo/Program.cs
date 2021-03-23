using System;
using System.Collections.Generic;

namespace CSP_genetic_algo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting constraint solving using a genetic algorithm.");
            
            ConstraintSolver.FitnessFunction fitnessFunctionDelegate = ConstraintSolver.CalculateFitness;
            Population initialPopulation = new Population();
            int populationSize = 20;
            
            //Generate an initial population of x with random values within the domain
            for (int cnt = 0; cnt < populationSize; cnt++)
            {
                Configuration individual = new Configuration();
                individual.SetRandomValuesFromDomain();
                initialPopulation.Members.Add(individual);
            }
            Configuration solution = ConstraintSolver.SolveWithGeneticAlgorithm(initialPopulation, fitnessFunctionDelegate);
            if (solution != null)
            {
                Console.WriteLine("Solution found: " + solution);
            }
            else
            {
                Console.WriteLine("No Solution found within the time limit/iteration limit!");
            }
            Console.Write("Press any key to exit.");
            Console.ReadKey();
        }
    }
}