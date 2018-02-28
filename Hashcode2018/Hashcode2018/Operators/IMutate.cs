namespace Hashcode2018.Operators
{
    public interface IMutate<T>
    {
        T Mutate(T chromosome);
    }
}