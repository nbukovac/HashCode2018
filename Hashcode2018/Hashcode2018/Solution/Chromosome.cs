namespace Hashcode2018.Solution
{
    public class Chromosome : IChromosome<double[]>
    {
        public double Fitness { get; set; }
        public double[] Values { get; set; }

        public Chromosome(int numberOfValues)
        {
            Values = new double[numberOfValues];
        }

        public double this[int index]
        {
            get => Values[index];
            set => Values[index] = value;
        }
    }
}