using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Hashcode2018.Algorithm;
using Hashcode2018.Fitness;
using Hashcode2018.Models;
using Hashcode2018.Operators.Crossover;
using Hashcode2018.Operators.Mutation;
using Hashcode2018.Operators.Selection;
using Hashcode2018.Solution;

namespace Hashcode2018
{
    public static class Program
    {
        
        private const int PopulationSize = 30;
        private const double FitnessTerminator = -10e9;
        private const int IterationLimit = 50_000;
        private const double MutationProbability = 0.05;
        private const int TournamentSize = 3;
        private const string FilePath = "/home/nikola/Projekti/HashCode2018/data/c_no_hurry.in";
        private const string OutputPath = "/home/nikola/Projekti/HashCode2018/data/c_no_hurry.out";
        
        public static void Main(string[] args)
        {
            FitnessFunction.Rides = ParseData(out int fleetSize, out int bonus, out int steps);
            Chromosome.FleetSize = fleetSize;
            FitnessFunction.NumberOfSteps = steps;
            FitnessFunction.Bonus = bonus;
            
            var mutation = new UniformMutation(MutationProbability); 
            var selection = new KTournamentSelection(TournamentSize);
            var crossover = new UniformCrossover();
            var fitnessFunction = new FitnessFunction();

            var geneticAlgorithm = new EliminationGeneticAlgorithm(mutation, selection, crossover, fitnessFunction,
                IterationLimit, FitnessTerminator, PopulationSize, FitnessFunction.Rides.Count);

            var optimum = geneticAlgorithm.FindOptimum();
            
            WriteOutput(optimum);
        }

        private static void WriteOutput(Chromosome optimum)
        {
            var vehicles = new List<Vehicle>();
            var rides = FitnessFunction.Rides.OrderBy(x => x.EarliestStart);

            foreach (var ride in rides)
            {
                var vehicleIndex = (int) optimum[ride.Id];

                if (vehicles.Any(x => x.Id == vehicleIndex))
                {
                    var vehicle = vehicles.First(x => x.Id == vehicleIndex);
                    vehicle.AddRideId(ride.Id);
                }
                else
                {
                    var vehicle = new Vehicle {Id = vehicleIndex};
                    vehicle.AddRideId(ride.Id);
                    vehicles.Add(vehicle);
                }
            }
            
            /*
            for (int i = 0; i < FitnessFunction.Rides.Count; i++)
            {
                var vehicleIndex = (int) optimum[i];

                if (vehicles.Any(x => x.Id == vehicleIndex))
                {
                    var vehicle = vehicles.First(x => x.Id == vehicleIndex);
                    vehicle.AddRideId(i);
                }
                else
                {
                    var vehicle = new Vehicle {Id = vehicleIndex};
                    vehicle.AddRideId(i);
                    vehicles.Add(vehicle);
                }
            }*/

            vehicles = vehicles.OrderBy(x => x.Id).ToList();

            using (var writer = new StreamWriter(OutputPath))
            {
                for (int i = 0; i < Chromosome.FleetSize; i++)
                {
                    var vehicle = vehicles.FirstOrDefault(x => x.Id == i);

                    if (vehicle == null)
                    {
                        writer.WriteLine("0");
                    }
                    else
                    {
                        var sb = new StringBuilder();

                        foreach (var rideId in vehicle.RideIds)
                        {
                            sb.Append(rideId).Append(' ');
                        }

                        var vehicleRides = vehicle.RideIds.Count;
                    
                        writer.WriteLine(vehicleRides + " " + sb.ToString().Trim());
                    }
                }
            }
        }
        
        private static IList<Ride> ParseData(out int fleetSize, out int bonus, out int steps)
        {
            var rides = new List<Ride>();
            
            using (var reader = new StreamReader(FilePath))
            {
                var line = reader.ReadLine();
                var lineSplit = line.Split(new char[0] { }, StringSplitOptions.RemoveEmptyEntries);

                fleetSize = int.Parse(lineSplit[2]);
                bonus = int.Parse(lineSplit[4]);
                steps = int.Parse(lineSplit[5]);
                
                int count = 0;
                
                while ((line = reader.ReadLine()) != null)
                {
                    lineSplit = line.Split(new char[0] { }, StringSplitOptions.RemoveEmptyEntries);
                    
                    var ride = new Ride()
                    {
                        Id = count,
                        A = int.Parse(lineSplit[0]),
                        B = int.Parse(lineSplit[1]),
                        X = int.Parse(lineSplit[2]),
                        Y = int.Parse(lineSplit[3]),
                        EarliestStart = int.Parse(lineSplit[4]),
                        LatestFinish = int.Parse(lineSplit[5]),
                    };

                    rides.Add(ride);
                    count++;
                }
            }

            return rides;
        }
    }
}