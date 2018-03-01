using System;
using System.Collections.Generic;
using System.Linq;
using Hashcode2018.Models;
using Hashcode2018.Solution;

namespace Hashcode2018.Fitness
{
    public class FitnessFunction : IFitnessFunction<Chromosome>
    {
        public static int NumberOfSteps = 0;
        public static int Bonus = 0;
        public static IList<Ride> Rides;
        
        public double CalculateFitness(Chromosome chromosome)
        {
            var vehicleRide = new Dictionary<int, List<Tuple<int, int>>>();
            var sum = 0.0;

            for (int i = 0; i < Rides.Count; i++)
            {
                var vehicleIndex = (int)chromosome[i];
                var ride = Rides[i];

                if (vehicleRide.ContainsKey(vehicleIndex))
                {
                    var check = vehicleRide[vehicleIndex]
                        .Any(x => x.Item1 >= ride.EarliestStart && x.Item2 <= ride.LatestFinish);

                    if (!check)
                    {
                        sum += Math.Abs(ride.X - ride.A) + Math.Abs(ride.Y - ride.B);
                        vehicleRide[vehicleIndex].Add(new Tuple<int, int>(ride.EarliestStart, ride.LatestFinish));
                    }
                    
                    var check2 = vehicleRide[vehicleIndex]
                        .Any(x => x.Item1 <= ride.LatestFinish && x.Item2 >= ride.LatestFinish);
                    
                    if (check2)
                    {
                        sum -= Bonus;
                    }
                }
                else
                {
                    vehicleRide.Add(vehicleIndex, new List<Tuple<int, int>>());
                    vehicleRide[vehicleIndex].Add(new Tuple<int, int>(ride.EarliestStart, ride.LatestFinish));
                    sum += Math.Abs(ride.X - ride.A) + Math.Abs(ride.Y - ride.B);
                }
            }
            
            return sum;
        }
    }
}