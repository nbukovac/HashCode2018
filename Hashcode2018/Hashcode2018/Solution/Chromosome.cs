using System.Text;
using Hashcode2018.Utilities;

namespace Hashcode2018.Solution
{
    public class Chromosome : IChromosome<double[]>
    {
        public double Fitness { get; set; }
        public double[] Values { get; set; }
        public static int FleetSize = 0;

        public Chromosome(int numberOfValues)
        {
            Values = new double[numberOfValues];

            for (int i = 0; i < numberOfValues; i++)
            {
                Values[i] = UtilityMethods.Random.Next(FleetSize);
            }
        }

        public double this[int index]
        {
            get => Values[index];
            set => Values[index] = value;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var value in Values)
            {
                sb.Append(value).Append(" ");
            }

            return Fitness.ToString();
        }
    }
}