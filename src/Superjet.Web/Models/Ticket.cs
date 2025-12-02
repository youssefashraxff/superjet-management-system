namespace Superjet.Web.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public string SeatNo { get; set; }
        public DateTime BookingDate { get; set; }
        public TicketStatus Status { get; set; }

        // Link to Route
        public int RouteId { get; set; }
        public Route Route { get; set; }

        // Link to User (Passenger)
        public int UserId { get; set; }
        public User User { get; set; }
    }

    public enum TicketStatus
    {
        Booked,
        Cancelled
    }
}
