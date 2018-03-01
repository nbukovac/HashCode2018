using System.Collections.Generic;

namespace Hashcode2018.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        public List<int> RideIds { get; set; }

        public Vehicle()
        {
            RideIds = new List<int>();
        }

        public void AddRideId(int id)
        {
            RideIds.Add(id);
        }
    }
}