namespace Superjet.Web.Models
{
    public class Discount
    {
        public int DiscountId { get; set; }
        public string Code { get; set; }
        public decimal Percentage { get; set; }
        public DateTime ValidUntil { get; set; }

        
    }
}
