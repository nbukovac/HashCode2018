namespace Hashcode2018.Fitness
{
    public interface IFitnessFunction<T>
    {
        double CalculateFitness(T chromosome);
    }
}