using System;
using System.Linq;
using Hashcode2018.Fitness;
using Hashcode2018.Operators;
using Hashcode2018.Solution;

namespace Hashcode2018.Algorithm
{
    public class EliminationGeneticAlgorithm : GeneticAlgorithm<Chromosome>
    {
        public EliminationGeneticAlgorithm(IMutate<Chromosome> mutation, ISelection<Chromosome> selection, ICrossover<Chromosome> crossover,
            IFitnessFunction<Chromosome> fitnessFunction, int iterationLimit, double fitnessTerminator, int populationSize) 
            : base(mutation, selection, crossover, fitnessFunction, iterationLimit, fitnessTerminator, populationSize)
        {
            Population = new Population<Chromosome>(populationSize);
            
            InitializePopulation();
        }

        private void InitializePopulation()
        {
            for (var i = 0; i < PopulationSize; i++)
            {
                var chromosome = new Chromosome(5);
                chromosome.Fitness = FitnessFunction.CalculateFitness(chromosome);
                Population.Add(chromosome);
            }
        }
        
        public override Chromosome FindOptimum()
        {
            var best = new Chromosome(5) { Fitness = double.MaxValue };

            for (var i = 0; i < IterationLimit; i++)
            {
                var selectedFromPopulation = Selection.Select(Population);
                
                var childChromosome = Crossover.Cross(selectedFromPopulation[0], selectedFromPopulation[1]);
                childChromosome = Mutation.Mutate(childChromosome);
                childChromosome.Fitness = FitnessFunction.CalculateFitness(childChromosome);
                
                Population.Remove(selectedFromPopulation[2]);
                Population.Add(childChromosome);

                var populationBest = double.MaxValue;
                var bestIndex = 0;
                var index = 0;

                foreach (var chromosome in Population)
                {
                    if (chromosome.Fitness < populationBest)
                    {
                        populationBest = chromosome.Fitness;
                        bestIndex = index;
                    }

                    index++;
                }

                if (populationBest < best.Fitness)
                {
                    best = Population.ElementAt(bestIndex);
                    Console.WriteLine("Iteration: " + i + ", Best fitness: " + populationBest);
                }

                if (populationBest < FitnessTerminator)
                {
                    break;
                }
            }
            
            return best;
        }
    }
}