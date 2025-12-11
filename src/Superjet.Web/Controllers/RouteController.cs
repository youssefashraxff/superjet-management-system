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
            _context.Routes.Update(route);
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

}
