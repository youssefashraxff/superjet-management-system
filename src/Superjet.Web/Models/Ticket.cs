namespace Superjet.Web.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string SeatNo { get; set; }
        public DateTime BookingDate { get; set; }
        public TicketStatus Status { get; set; }
        // Link to User (Passenger)
        public int UserId { get; set; }
        public User User { get; set; }
        public Route_travel Route {get; set;}
        public int RouteId { get; set; }
        public Discount Discount{get;set;}
        public int DiscountId{get;set;}
    }

    public enum TicketStatus
    {
        Booked,
        Cancelled
    }
}
