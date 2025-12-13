using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Superjet.Web.Data;
using Superjet.Web.Models;

namespace Superjet.Web.Controllers
{
    public class RouteController : Controller
    {
        private readonly AppDbContext _context;

        public RouteController(AppDbContext context)
        {
            _context = context;
        }

        // View Routes
        public IActionResult Index()
        {
            var routes = _context.Routes
                                 .Include(r => r.Bus)
                                 .ToList();

            return PartialView(routes);
        }

        // Create Route
        [HttpPost]
        public IActionResult Create([FromBody] Route_travel route)
        {
            if (route == null)
                return BadRequest();

            _context.Routes.Add(route);
            _context.SaveChanges();

            var routes = _context.Routes
                                 .Include(r => r.Bus)
                                 .ToList();

            return PartialView("Index", routes);
        }

        // Update Route
        [HttpPost]
        public IActionResult Edit([FromBody] Route_travel route)
        {
            var existing = _context.Routes.Find(route.Id);
            if (existing == null)
                return NotFound();

            existing.Origin = route.Origin;
            existing.Destination = route.Destination;
            existing.DepartureTime = route.DepartureTime;
            existing.ArrivalTime = route.ArrivalTime;
            existing.Price = route.Price;

            _context.SaveChanges();

            var routes = _context.Routes
                                 .Include(r => r.Bus)
                                 .ToList();

            return PartialView("Index", routes);
        }

        // Delete Route
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var route = _context.Routes.Find(id);
            if (route == null)
                return NotFound();

            _context.Routes.Remove(route);
            _context.SaveChanges();

            var routes = _context.Routes
                                 .Include(r => r.Bus)
                                 .ToList();

            return PartialView("Index", routes);
        }
    
        [HttpPost]
        public IActionResult AssignBus([FromBody] AssignBusDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data");

            var route = _context.Routes.Find(dto.RouteId);
            if (route == null)
                return NotFound("Route not found");

            var bus = _context.Buses.Find(dto.BusId);
            if (bus == null)
                return NotFound("Bus not found");

             bool busBusy = _context.Routes.Any(r =>
                r.Id != dto.RouteId &&
                r.BusId == dto.BusId &&
                route.DepartureTime < r.ArrivalTime &&
                route.ArrivalTime > r.DepartureTime
            );

            if (busBusy)
            {
                return BadRequest("This bus is already assigned to another route at the same time. Please choose another bus.");
            }

            route.BusId = dto.BusId;
            _context.SaveChanges();

            return Ok();
        }
    }
}
