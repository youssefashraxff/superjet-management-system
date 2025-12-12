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
    public IActionResult Index()
    {
        var buses = _context.Buses.ToList();
        return PartialView(buses);
    }

    // Adding new bus
    [HttpPost]
    public IActionResult Create([FromBody] BusCreateDto dto)
    {
        if (dto == null) return BadRequest("Invalid bus data");

        // parse enum safely
        if (!Enum.TryParse<BusStatus>(dto.Status, true, out var status))
            status = BusStatus.Available;

        var bus = new Bus
        {
            BusNo = dto.BusNo,
            Model = dto.Model,
            Capacity = dto.Capacity,
            Status = status,
            // Routes list auto-initialized by the Bus model or EF
        };

        _context.Buses.Add(bus);
        _context.SaveChanges();

        var buses = _context.Buses.ToList();
        return PartialView("Index", buses);
    }
    //update bus details
    [HttpPost]
    public IActionResult Edit([FromBody] BusCreateDto dto)
    {
        if (dto == null) return BadRequest("Invalid bus data");

        var bus = _context.Buses.FirstOrDefault(b => b.Id == dto.Id);
        if (bus == null) return NotFound();

        if (!Enum.TryParse<BusStatus>(dto.Status, true, out var status))
            status = BusStatus.Available;

        bus.BusNo = dto.BusNo;
        bus.Model = dto.Model;
        bus.Capacity = dto.Capacity;
        bus.Status = status;

        _context.SaveChanges();

        var buses = _context.Buses.ToList();
        return PartialView("Index", buses);
    }

    [HttpPost]
    public IActionResult SetMaintenanceStatus(int busId, string status)
    {
        var bus = _context.Buses.Find(busId);
        if (bus == null) return NotFound();

        if (!Enum.TryParse<BusStatus>(status, true, out var parsedStatus))
            parsedStatus = BusStatus.Available;

        bus.Status = parsedStatus;

        _context.Buses.Update(bus);
        _context.SaveChanges();

        var buses = _context.Buses.ToList();
        return PartialView("Index", buses);
    }

    //delete bus
    [HttpPost]
    public IActionResult Delete(int id)
    {
        var bus = _context.Buses
                        .Include(b => b.Routes)
                        .FirstOrDefault(b => b.Id == id);

        if (bus == null)
            return NotFound();

        if (bus.Routes.Any())
        {
            Response.StatusCode = 400;
            return Content("This bus cannot be deleted because it is assigned to routes.");
        }

        _context.Buses.Remove(bus);
        _context.SaveChanges();

        var buses = _context.Buses.ToList();
        return PartialView("Index", buses);
    }

    //assign a route to bus (add this route to the list of routes that the bus have)
    [HttpPost]
    public IActionResult AssignRouteToBus(int routeId, int busId)
    {
        var route = _context.Routes.Find(routeId);
        if (route == null)
            return NotFound();

        var bus = _context.Buses
            .Include(b => b.Routes)   // load existing routes
            .FirstOrDefault(b => b.Id == busId);

        // Assign a route to bus
        bus.Routes.Add(route);
        route.BusId = busId;

        _context.SaveChanges();

        var buses = _context.Buses.ToList();
        return PartialView("Index", buses);
    }
}