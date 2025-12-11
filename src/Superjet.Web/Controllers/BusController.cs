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
    public async Task<IActionResult> Create(Bus bus)
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
    public async Task<IActionResult> Edit(Bus bus)
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

    //assign a route to bus (add this route to the list of routes that the bus have)
    [HttpPost]
    public async Task<IActionResult> AssignRouteToBus(int routeId, int busId)
        {
            var route = await _context.Routes.FindAsync(routeId);
            if (route == null)
                return NotFound();

           var bus = await _context.Buses
           .Include(b => b.Routes)   // load existing routes
           .FirstOrDefaultAsync(b => b.Id == busId);

            // Assign a route to bus
            bus.Routes.Add(route);
            route.BusId=busId;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }