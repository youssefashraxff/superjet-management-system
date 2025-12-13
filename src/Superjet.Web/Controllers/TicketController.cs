using System.Runtime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Superjet.Web.Data;
using Superjet.Web.Models;

namespace Superjet.Web.Controllers
{
    public class TicketController : Controller
    {
        private readonly AppDbContext _context;

        public TicketController(AppDbContext context)
        {
            _context = context;
        }

        // LOAD TICKETS OF A ROUTE  (ADMIN VIEW)
        [HttpGet]
        public IActionResult Index(int routeId)
        {
            var tickets = _context.Tickets
                .Where(t => t.RouteId == routeId)
                .Include(t => t.User)
                .Include(t => t.Route)
                .ToList();

            return PartialView("Index", tickets);
        }

        // TICKET DETAILS (ADMIN VIEW)
        [HttpGet]
        public IActionResult Details(int id)
        {
            var ticket = _context.Tickets
                .Include(t => t.User)
                .Include(t => t.Route)
                .Include(t => t.Discount)
                .FirstOrDefault(t => t.Id == id);

            if (ticket == null) return NotFound();

            return PartialView("Details", ticket);
        }

        // CANCEL TICKET
        [HttpPost]
        public IActionResult Cancel(int id)
        {
            var ticket = _context.Tickets.FirstOrDefault(t => t.Id == id);
            if (ticket == null) return NotFound();

            ticket.Status = TicketStatus.Cancelled;
            _context.SaveChanges();

            return RedirectToAction("Profile", "User");
        }
        [HttpPost]
        public IActionResult Checkout()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return Unauthorized();

            var cart = _context.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Route)
                .Include(c => c.Discount)
                .FirstOrDefault(c => c.UserId == userId);

            if (cart == null || !cart.Items.Any())
                return BadRequest("Cart is empty");

            foreach (var item in cart.Items)
            {
                for (int i = 0; i < item.Quantity; i++)
                {
                    var ticket = new Ticket
                    {
                        UserId = userId.Value,
                        RouteId = item.RouteId,
                        BookingDate = DateTime.Now,
                        Status = TicketStatus.Booked,
                        SeatNo = GenerateSeatNo(item.RouteId),
                        DiscountId = cart.DiscountId
                    };

                    _context.Tickets.Add(ticket);
                }
            }

            // Clear cart
            _context.CartItems.RemoveRange(cart.Items);
            cart.DiscountId = null;
            cart.Discount = null;

            _context.SaveChanges();

            return Ok();
        }
        private string GenerateSeatNo(int routeId)
        {
            int bookedSeats = _context.Tickets
                .Count(t => t.RouteId == routeId);

            return (bookedSeats + 1).ToString();
        }
    }
}