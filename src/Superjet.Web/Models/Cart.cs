namespace Superjet.Web.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public List<CartItem> Items { get; set; } = new();
        public int? DiscountId { get; set; }
        public Discount Discount { get; set; }
        public decimal TotalPrice =>
        Discount == null
            ? Items.Sum(i => i.Route.Price * i.Quantity)
            : Items.Sum(i => i.Route.Price * i.Quantity) *
              (1 - Discount.Percentage / 100);

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}