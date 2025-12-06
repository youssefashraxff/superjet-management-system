namespace Superjet.Web.Models
{
    public class Route_travel
    {
        public int Id { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public decimal Distance { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Price { get; set; }
        public Bus Bus { get; set; }
        public int BusId { get; set; }
        public List<Ticket> Tickets {get; set;}
    }
}
