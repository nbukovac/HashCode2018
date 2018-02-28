using System;
using Hashcode2018.Algorithm;
using Hashcode2018.Fitness;
using Hashcode2018.Operators.Crossover;
using Hashcode2018.Operators.Mutation;
using Hashcode2018.Operators.Selection;

namespace Hashcode2018
{
    public static class Program
    {
        
        private const int PopulationSize = 30;
        private const double FitnessTerminator = 10e-9;
        private const int IterationLimit = 40_000;
        private const double MutationProbability = 0.01;
        private const int TournamentSize = 3;
        
        
        public static void Main(string[] args)
        {
            var mutation = new UniformMutation(MutationProbability); 
            var selection = new KTournamentSelection(TournamentSize);
            var crossover = new ArithmeticCrossover();
            var fitnessFunction = new FitnessFunction();

            var geneticAlgorithm = new EliminationGeneticAlgorithm(mutation, selection, crossover, fitnessFunction,
                IterationLimit, FitnessTerminator, PopulationSize);

            var optimum = geneticAlgorithm.FindOptimum();
        }
    }
}