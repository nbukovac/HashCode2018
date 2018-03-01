using Hashcode2018.Solution;
using Hashcode2018.Utilities;

namespace Hashcode2018.Operators.Crossover
{
    public class UniformCrossover : ICrossover<Chromosome>
    {
        public Chromosome Cross(Chromosome chromosome1, Chromosome chromosome2)
        {
            var childChromosome = new Chromosome(chromosome1.Values.Length);

            for (var i = 0; i < chromosome1.Values.Length; i++)
            {
                var random = UtilityMethods.Random.NextDouble();

                if (random < 0.5)
                {
                    childChromosome[i] = chromosome1[i];
                }
                else
                {
                    childChromosome[i] = chromosome2[i];
                }

            }

            return childChromosome;
        }
    }
}