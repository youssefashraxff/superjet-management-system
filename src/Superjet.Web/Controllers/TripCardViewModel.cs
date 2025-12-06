public class TripCardViewModel
{
    public int RouteId { get; set; }

    public string Origin { get; set; }
    public string Destination { get; set; }

    public DateTime Departure { get; set; }
    public DateTime Arrival { get; set; }

    public string BusModel { get; set; }
    public string BusNumber { get; set; }
    public int RemainingSeats { get; set; }
    public decimal Price { get; set; }
}