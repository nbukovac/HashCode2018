namespace Hashcode2018.Solution
{
    public interface IChromosome<T>
    {
        double Fitness { get; set; }

        T Values { get; set; }
    }
}