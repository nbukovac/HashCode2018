namespace Hashcode2018.Models
{
    public class Ride
    {
        public int Id { get; set; }
        
        public int A { get; set; }
        public int B { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public int EarliestStart { get; set; }
        public int LatestFinish { get; set; }

        public int VehicleId { get; set; }
    }
}