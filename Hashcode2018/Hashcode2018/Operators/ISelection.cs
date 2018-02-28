using System.Collections.Generic;
using Hashcode2018.Solution;

namespace Hashcode2018.Operators
{
    public interface ISelection<T>
    {
        IList<T> Select(Population<T> population);
    }
}