using System;
using System.Linq;
using Hashcode2018.Fitness;
using Hashcode2018.Operators;
using Hashcode2018.Solution;

namespace Hashcode2018.Algorithm
{
    public class EliminationGeneticAlgorithm : GeneticAlgorithm<Chromosome>
    {
        public int ChromosomeSize { get; set; }
        
        public EliminationGeneticAlgorithm(IMutate<Chromosome> mutation, ISelection<Chromosome> selection, ICrossover<Chromosome> crossover,
            IFitnessFunction<Chromosome> fitnessFunction, int iterationLimit, double fitnessTerminator, int populationSize, int chromosomeSize) 
            : base(mutation, selection, crossover, fitnessFunction, iterationLimit, fitnessTerminator, populationSize)
        {
            Population = new Population<Chromosome>(populationSize);
            ChromosomeSize = chromosomeSize;
            
            InitializePopulation();
        }

        private void InitializePopulation()
        {
            for (var i = 0; i < PopulationSize; i++)
            {
                var chromosome = new Chromosome(ChromosomeSize);
                chromosome.Fitness = FitnessFunction.CalculateFitness(chromosome);
                Population.Add(chromosome);
            }
        }
        
        public override Chromosome FindOptimum()
        {
            var best = new Chromosome(ChromosomeSize) { Fitness = double.NegativeInfinity };

            for (var i = 0; i < IterationLimit; i++)
            {
                var selectedFromPopulation = Selection.Select(Population);
                
                var childChromosome = Crossover.Cross(selectedFromPopulation[0], selectedFromPopulation[1]);
                childChromosome = Mutation.Mutate(childChromosome);
                childChromosome.Fitness = FitnessFunction.CalculateFitness(childChromosome);
                
                Population.Remove(selectedFromPopulation[2]);
                Population.Add(childChromosome);

                var populationBest = 0.0;
                var bestIndex = 0;
                var index = 0;

                foreach (var chromosome in Population)
                {
                    if (chromosome.Fitness > populationBest)
                    {
                        populationBest = chromosome.Fitness;
                        bestIndex = index;
                    }

                    index++;
                }

                if (populationBest > best.Fitness)
                {
                    best = Population.ElementAt(bestIndex);
                    Console.WriteLine("Iteration: " + i + ", Best fitness: " + populationBest);
                }

                
            }
            
            return best;
        }
    }
}