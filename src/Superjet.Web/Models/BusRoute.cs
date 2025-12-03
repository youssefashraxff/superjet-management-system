namespace Superjet.Web.Models
{
    public class BusRoute
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public decimal Distance { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Price { get; set; }
        public List<Bus> Buses {get; set;}
        public List<Ticket> Tickets {get; set;}
    }
}
