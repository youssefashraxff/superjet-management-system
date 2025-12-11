namespace Superjet.Web.Models
{
    public class Discount
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public decimal Percentage { get; set; }
        public DateTime ValidUntil { get; set; }

        public List<Ticket> Tickets {get; set;}
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
