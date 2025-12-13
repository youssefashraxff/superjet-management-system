using Microsoft.AspNetCore.Mvc;
using Superjet.Web.Data;     
using Superjet.Web.Models;   
using Microsoft.EntityFrameworkCore;
public class CartController : Controller
{
    private readonly AppDbContext _context;

    public CartController(AppDbContext context)
    {
        _context = context;
    }
    // View Cart
    public IActionResult Index()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null) return Unauthorized();

        var cart = _context.Carts
            .Include(c => c.Items)
                .ThenInclude(i => i.Route)
                    .ThenInclude(r => r.Bus)
                    .Include(c => c.Discount)
            .FirstOrDefault(c => c.UserId == userId);

        return View(cart);
    }
    // Add item to cart
    [HttpPost("Cart/Add/{routeId}")]
    public IActionResult Add(int routeId)
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null || !_context.Users.Any(u => u.Id == userId))
        {
            HttpContext.Session.Clear();
            return Unauthorized();
        }
        
        
        var route = _context.Routes.IgnoreQueryFilters().FirstOrDefault(r => r.Id == routeId);
        if (route == null)
            return NotFound(routeId);

        
        var cart = _context.Carts
            .Include(c => c.Items)
            .FirstOrDefault(c => c.UserId == userId);

        if (cart == null)
        {
            cart = new Cart
            {
                UserId = userId.Value,
                CreatedAt = DateTime.Now
            };

            _context.Carts.Add(cart);
            _context.SaveChanges(); 
        }

        
        if (cart.Items == null) cart.Items = new List<CartItem>();

        cart.Items.Add(new CartItem
        {
            RouteId = routeId,
            Quantity = 1
        });
        _context.SaveChanges();

        
        var updatedCart = _context.Carts
            .Include(c => c.Items)
            .ThenInclude(i => i.Route)
            .ThenInclude(r => r.Bus)
            .Include(c => c.Discount)
            .FirstOrDefault(c => c.Id == cart.Id);


        return PartialView("~/Views/Cart/Index.cshtml", updatedCart);
        
    }
    // Delete from cart
    [HttpPost("Cart/Remove/{itemId}")]
    public IActionResult Remove(int itemId)
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
            return Unauthorized();

        var item = _context.CartItems
            .Include(i => i.Cart)
            .Include(i => i.Route)
                .ThenInclude(r => r.Bus)
            .FirstOrDefault(i =>
                i.Id == itemId &&
                i.Cart.UserId == userId);

        if (item == null)
            return NotFound();

        if (item.Quantity > 1)
        {
            item.Quantity--;
        }
        else
        {
            _context.CartItems.Remove(item);
        }

        _context.SaveChanges();

        var updatedCart = _context.Carts
            .Include(c => c.Items)
                .ThenInclude(i => i.Route)
                    .ThenInclude(r => r.Bus)
                    .Include(c => c.Discount)
            .FirstOrDefault(c => c.UserId == userId);

        return PartialView("Index", updatedCart);
    }
    [HttpPost]
    public IActionResult ApplyDiscount([FromBody] ApplyDiscountDto dto)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return Unauthorized();

        var cart = _context.Carts
            .Include(c => c.Items)
            .ThenInclude(i => i.Route)
            .ThenInclude(r => r.Bus)
            .Include(c => c.Discount)
            .FirstOrDefault(c => c.UserId == userId);

        if (cart == null)
            return BadRequest("Cart not found");

        var discount = _context.Discounts.FirstOrDefault(d =>
            d.Code == dto.Code &&
            d.StartDate <= DateTime.Now &&
            d.EndDate >= DateTime.Now
        );

        if (discount == null)
            return BadRequest("Invalid or expired discount code");

        cart.DiscountId = discount.Id;
        cart.Discount = discount;

        _context.SaveChanges();

        return PartialView("Index", cart);
    }
}