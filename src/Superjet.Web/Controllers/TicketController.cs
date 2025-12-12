using Microsoft.AspNetCore.Mvc;
using Superjet.Web.Data;     
using Superjet.Web.Models;   
using Microsoft.EntityFrameworkCore;
namespace Superjet.Web.Controllers
{
    public class TicketController : Controller
    {
        private readonly AppDbContext _context;

        public TicketController(AppDbContext context)
        {
            _context = context;
        }

        //view all tickets
        [HttpGet]
        public IActionResult Index(int routeId)
        {
            var tickets = _context.Tickets
            .Where(t => t.RouteId == routeId)
            .Include(t => t.User)     // ← add this
            .Include(t => t.Route)
            .ToList();

            return PartialView(tickets);
        }

        //Get Ticket Details
        [HttpGet]
        public IActionResult Details(int id)
        {
            var ticket = _context.Tickets
                .Include(t => t.Route)
                .FirstOrDefault(t => t.Id == id);

            if (ticket == null) return NotFound();

            return PartialView(ticket);
        }

        //Book Ticket
        [HttpPost]
        public IActionResult Create(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.BookingDate = DateTime.Now;
                ticket.Status = TicketStatus.Booked;
                _context.Tickets.Add(ticket);
                _context.SaveChanges();
                return RedirectToAction(nameof(Details), new { id = ticket.Id });//redirect the user to the Details page for that ticket so they can see the updated information.
            }
            return View(ticket);
        }

        //Update Ticket
        [HttpPost]
        public IActionResult Edit(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _context.Tickets.Update(ticket);
                _context.SaveChanges();
                return RedirectToAction(nameof(Details), new { id = ticket.Id });
            }
            return View(ticket);
        }

        //Cancel Ticket
        [HttpPost]
        public IActionResult Cancel(int id)
        {
            var ticket = _context.Tickets.FirstOrDefault(t => t.Id == id);
            if (ticket == null) return NotFound();

            ticket.Status = TicketStatus.Cancelled;
            _context.SaveChanges();

            return RedirectToAction(nameof(Details), new { id = ticket.Id });
        }
    }
}
