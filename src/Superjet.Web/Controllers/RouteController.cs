using Microsoft.AspNetCore.Mvc;
using Superjet.Web.Data;     
using Superjet.Web.Models;   
using Microsoft.EntityFrameworkCore;

public class RouteController : Controller
{
    private readonly AppDbContext _context;

    public RouteController(AppDbContext context)
    {
        _context = context;
    }

    // View Routes
    public async Task<IActionResult> Index()
    {
        var routes = await _context.Routes.ToListAsync();
        return View(routes);
    }

    // Create Route
    [HttpPost]
    public async Task<IActionResult> Create(Route_travel route)
    {
        if (ModelState.IsValid)//all attributes are valid and filled correctly
        {
            _context.Routes.Add(route);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(route);
    }

    // Update Route
    [HttpPost]
    public async Task<IActionResult> Edit(Route_travel route)
    {
        if (ModelState.IsValid)
        {
            _context.Route_travels.Update(route);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(route);
    }

    //Delete Route
    public async Task<IActionResult> Delete(int id)
    {
        var route = await _context.Routes.FindAsync(id);
        if (route == null) return NotFound();
        _context.Routes.Remove(route);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    //Assign Bus to route (each route have one bus)
    [HttpPost]
    public async Task<IActionResult> AssignBusToRoute(int routeId, int busId)
    {
        var route = await _context.Routes.FindAsync(routeId);
        if (route == null) return NotFound();

        var bus = await _context.Buses.FindAsync(busId);
        if (bus == null) return NotFound();

        route.BusId = busId;
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
