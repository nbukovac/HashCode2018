using Hashcode2018.Solution;
using Hashcode2018.Utilities;

namespace Hashcode2018.Operators.Crossover
{
    public class ArithmeticCrossover : ICrossover<Chromosome>
    {
        public Chromosome Cross(Chromosome chromosome1, Chromosome chromosome2)
        {
            var childChromosome = new Chromosome(chromosome1.Values.Length);

            for (var i = 0; i < chromosome1.Values.Length; i++)
            {
                var random = UtilityMethods.Random.NextDouble();

                childChromosome[i] = chromosome1[i] * random + chromosome2[i] * (1 - random);
            }

            return childChromosome;
        }
    }
}