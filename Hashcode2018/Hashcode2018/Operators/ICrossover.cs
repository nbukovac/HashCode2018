namespace Hashcode2018.Operators
{
    public interface ICrossover<T>
    {
        T Cross(T chromosome1, T chromosome2);
    }
}