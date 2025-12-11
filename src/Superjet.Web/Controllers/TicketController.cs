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
    public async Task<IActionResult> Index()
    {
        var tickets = await _context.Tickets
        .Include(t => t.Route)
        .ToListAsync();
        return View(tickets);
    }

    //Get Ticket Details
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var ticket = await _context.Tickets
            .Include(t => t.Route)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (ticket == null) return NotFound();

        return View(ticket);
    }

    //Book Ticket
    [HttpPost]
    public async Task<IActionResult> Create(Ticket ticket)
    {
        if (ModelState.IsValid)
        {
            ticket.BookingDate = DateTime.Now;
            ticket.Status = TicketStatus.Booked;
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = ticket.Id });//redirect the user to the Details page for that ticket so they can see the updated information.
        }
        return View(ticket);
    }

    //Update Ticket
    [HttpPost]
    public async Task<IActionResult> Edit(Ticket ticket)
    {
        if (ModelState.IsValid)
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = ticket.Id });
        }
        return View(ticket);
    }

    //Cancel Ticket
    [HttpPost]
    public async Task<IActionResult> Cancel(int id)
    {
        var ticket = await _context.Tickets.FindAsync(id);
        if (ticket == null) return NotFound();

        ticket.Status = TicketStatus.Cancelled;
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Details), new { id = ticket.Id });
    }
    //apply discount function
}

}
