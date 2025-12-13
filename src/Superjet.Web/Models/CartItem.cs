namespace Superjet.Web.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        public int CartId { get; set; }
        public Cart Cart { get; set; }

        public int RouteId { get; set; }
        public Route_travel Route { get; set; }
        public int Quantity { get; set; } = 1; // number of seats

    }
}