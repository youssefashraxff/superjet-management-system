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

            return Details(id);
        }
    }
}