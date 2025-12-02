namespace Superjet.Web.Models
{
    public class Bus
    {
        public int Id { get; set; }
        public string BusNo { get; set; }
        public string Model { get; set; }
        public int Capacity { get; set; }
        public BusStatus Status { get; set; }

    
    }

    public enum BusStatus
    {
        Available,
        Maintenance,
        OnTrip
    }
}
