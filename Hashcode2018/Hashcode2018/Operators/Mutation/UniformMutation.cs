using Hashcode2018.Solution;
using Hashcode2018.Utilities;

namespace Hashcode2018.Operators.Mutation
{
    public class UniformMutation : IMutate<Chromosome>
    {
        private readonly double _mutationProbability;

        public UniformMutation(double mutationProbability)
        {
            _mutationProbability = mutationProbability;
        }
        
        public Chromosome Mutate(Chromosome chromosome)
        {
            for (var i = 0; i < chromosome.Values.Length; i++)
            {
                chromosome[i] = UtilityMethods.Random.NextDouble() * chromosome[i];
            }

            return chromosome;
        }
    }
}