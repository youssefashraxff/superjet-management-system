using Microsoft.AspNetCore.Mvc;
using Superjet.Web.Data;     
using Superjet.Web.Models;   
using Microsoft.EntityFrameworkCore;


public class BusController : Controller
{
     private readonly AppDbContext _context;
        public BusController(AppDbContext context)
        {
            this._context = context;
        }
    //show all buses
    public async Task<IActionResult> Index()
    {
        var buses = await _context.Buses.ToListAsync();
        return View(buses);
    }

    // Adding new bus
    [HttpPost]
    public async Task<IActionResult> CreateNewBus(Bus bus)
    {
        if (ModelState.IsValid)//make sure all attributes are entered and written correctly
        {
            _context.Buses.Add(bus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));//triggers a new HTTP request
        }

        return View(bus);//error message appears and Form reloads without erasing their input
    }

   //update bus details
    [HttpPost]
    public async Task<IActionResult> EditBusDetails(Bus bus)
    {
        if (ModelState.IsValid)
        {
            _context.Buses.Update(bus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(bus);
    }

    
    [HttpPost]
    public async Task<IActionResult> SetMaintenanceStatus(int busId, string status)
    {
        var bus = await _context.Buses.FindAsync(busId);
        if (bus == null) return NotFound();
        string statusString = "Available"; // could come from form input

        bus.Status = Enum.Parse<BusStatus>(statusString);

        _context.Buses.Update(bus);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    //delete bus
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var bus = await _context.Buses.FindAsync(id);
        if (bus == null) return NotFound();

        _context.Buses.Remove(bus);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    //assign bus to a route
    [HttpPost]
    public async Task<IActionResult> AssignToRoute(int busId, int routeId)
    {
        var bus = await _context.Buses
                .Include(b => b.Routes)  // include current routes
                .FirstOrDefaultAsync(b => b.Id == busId);

        var route = await _context.Routes.FindAsync(routeId);

            if (bus == null || route == null)
                return NotFound();

            // Add route to the bus's Routes collection
            if (!bus.Routes.Contains(route))
            {
                bus.Routes.Add(route);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
    }
}