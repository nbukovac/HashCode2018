using System.Collections.Generic;
using System.Linq;
using Hashcode2018.Solution;
using Hashcode2018.Utilities;

namespace Hashcode2018.Operators.Selection
{
    public class KTournamentSelection : ISelection<Chromosome>
    {
        private readonly int _k;

        public KTournamentSelection(int k)
        {
            _k = k;
        }
        
        public IList<Chromosome> Select(Population<Chromosome> population)
        {
            var selected = new HashSet<Chromosome>();

            while (selected.Count < _k)
            {
                var index = UtilityMethods.Random.Next(population.Size);
                
                selected.Add(population.Chromosomes.ElementAt(index));
            }

            return selected.OrderBy(x => x.Fitness).ToList();
        }
    }
}