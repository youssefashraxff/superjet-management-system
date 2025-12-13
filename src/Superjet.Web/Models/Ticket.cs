using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public User User { get; set; }
        [JsonIgnore]
        public Route_travel Route { get; set; }
        public int RouteId { get; set; }
        [JsonIgnore]
        public Discount Discount { get; set; }
        public int? DiscountId { get; set; }
        public decimal TotalPrice =>
        Discount == null
            ? Route.Price
            : Route.Price *
              (1 - Discount.Percentage / 100);
    }

    public enum TicketStatus
    {
        Booked,
        Cancelled
    }
}
