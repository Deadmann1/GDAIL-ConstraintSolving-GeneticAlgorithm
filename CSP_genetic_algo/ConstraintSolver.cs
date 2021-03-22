using System;
using System.Collections.Generic;

namespace CSP_genetic_algo
{
    public static class ConstraintSolver
    {
        private static int IdealFitness = 5;

        /*
         * Delegate to provide the genetic algorithm with potentially different fitness functions
         * In our case we could specify different constraints to be solved
         */
        public delegate int FitnessFunction(Configuration individual);


        /*
         * Genetic algorithm which over iterations solves the constraints and returns an individual which fulfills all constraints (if possible)
         */
        public static Configuration SolveWithGeneticAlgorithm(Population population,
            FitnessFunction fitnessFunction)
        {
            Configuration idealIndividual = null;
            long iteration = 0;


            // Repeat the genetic algorithm until we have found a solution or enough time/iteration have passed
            while (idealIndividual == null && iteration < 10000000000)
            {
                // New population of children
                Population newPopulation = new Population();

                // Calculate the fitness of each individual in the old population and sum the total fitness of the pop.
                // Also check if we have found a solution
                population.TotalFitness = 0;
                foreach (Configuration individual in population.Members)
                {
                    individual.Fitness = fitnessFunction(individual);
                    population.TotalFitness += individual.Fitness;

                    if (individual.Fitness == IdealFitness)
                    {
                        idealIndividual = individual;
                        break;
                    }
                }

                // For each individual in the population do the following:
                // Select 2 individuals randomly weighted by fitness
                // Cross them and then mutate the resulting child with a small probability
                // Build the new population with the resulting children
                foreach (Configuration individual in population.Members)
                {
                    Configuration selectedOne = SelectRandomIndividualByFitness(population);
                    Configuration selectedTwo = SelectRandomIndividualByFitness(population);
                    Configuration child = CrossIndividuals(selectedOne, selectedTwo);
                    child = MutateIndividual(child, 0.01);
                    newPopulation.Members.Add(child);
                }

                population = newPopulation;
                iteration++;
            }

            return idealIndividual;
        }

        /*
         * Mutates a trait of the individual based on a given probability
         */
        private static Configuration MutateIndividual(Configuration child, double probability)
        {
            Random rng = new Random();
            if (rng.NextDouble() >= probability)
            {
                child.MutateRandomTrait();
            }

            return child;
        }

        /*
         * Produces a child by crossing the two selected individuals of a population
         * There a several ways to cross 2 individuals, we use a variation of single-point crossover here
         * We get a point between the first and last trait of the parents and the traits left of the point including the
         * point are given from parent A to the child and the traits right of the point are given from parent B to the child
         */
        private static Configuration CrossIndividuals(Configuration a, Configuration b)
        {
            Random rng = new Random();
            int crossoverPoint = rng.Next(0, 4);
            return new Configuration(a, b, crossoverPoint);
        }

        /*
         * Calculates the fitness of any given individual of the CSP
         * Fitness is expressed in how many constraints are fulfilled
         * The higher the fitness the better a individual fits the goal
         */
        public static int CalculateFitness(Configuration individual)
        {
            int fitness = 5;

            //Constraint 1 
            if (individual.FourWheel && individual.Type != Configuration.TypeEnum.Xdrive)
            {
                fitness--;
            }

            //Constraint 2
            if (individual.Skibag && individual.Type == Configuration.TypeEnum.City)
            {
                fitness--;
            }

            //Constraint 3
            if (individual.Fuel == 4 && individual.Type != Configuration.TypeEnum.City)
            {
                fitness--;
            }

            //Constraint 4
            if (individual.Fuel == 6 && individual.Type == Configuration.TypeEnum.Xdrive)
            {
                fitness--;
            }

            //Constraint 5
            if (individual.Type == Configuration.TypeEnum.City && individual.Fuel == 10)
            {
                fitness--;
            }
            
            /*
            //Constraint 6
            if (individual.FourWheel != true)
            {
                fitness--;
            }

            //Constraint 7
            if (individual.Fuel != 6)
            {
                fitness--;
            }

            //Constraint 8
            if (individual.Type != Configuration.TypeEnum.City)
            {
                fitness--;
            }

            //Constraint 9
            if (individual.Skibag != true)
            {
                fitness--;
            }

            //Constraint 10
            if (individual.Pdc != true)
            {
                fitness--;
            }
            */
            
            return fitness;
        }


        /*
         * Generates a list where each individual gets its probability based on its fitness assigned, then draws an individual
         */
        private static Configuration SelectRandomIndividualByFitness(Population population)
        {
            Configuration selectedIndividual = null;
            Random rng = new Random();
            double diceRoll = rng.NextDouble();
            List<KeyValuePair<Configuration, double>> weightedIndividuals =
                new List<KeyValuePair<Configuration, double>>();

            // Calculate the probability of each individual based on its fitness 
            foreach (Configuration individual in population.Members)
            {
                weightedIndividuals.Add(new KeyValuePair<Configuration, double>(individual,
                    (double)individual.Fitness / population.TotalFitness));
            }

            double cumulative = 0.0;
            for (int i = 0; i < weightedIndividuals.Count; i++)
            {
                cumulative += weightedIndividuals[i].Value;
                if (diceRoll < cumulative)
                {
                    selectedIndividual = weightedIndividuals[i].Key;
                    break;
                }
            }

            return selectedIndividual;
        }
    }
}