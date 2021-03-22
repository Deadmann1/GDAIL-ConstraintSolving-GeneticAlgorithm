using System;
using System.Collections.Generic;

namespace CSP_genetic_algo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting constraint solving using an genetic algorithm.");
            
            ConstraintSolver.FitnessFunction fitnessFunctionDelegate = ConstraintSolver.CalculateFitness;
            Population initialPopulation = new Population();
            
            //Generate an initial population of x with random values withing the domain
            for (int cnt = 0; cnt < 10; cnt++)
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
        }
    }
}