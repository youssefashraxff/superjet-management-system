using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Superjet.Web.Data;
using Superjet.Web.Models;
namespace Superjet.Web.Controllers
{
    public class TripsController : Controller
    {
        private readonly AppDbContext context;
        public TripsController(AppDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View(new List<Route_travel>());
        }
        // public IActionResult Search(string origin, string destination, string date)
        // {
        //     DateTime selectedDate = DateTime.Parse(date);

        //     var routes = context.Routes
        //         .Where(r =>
        //             r.Origin == origin &&
        //             r.Destination == destination &&
        //             r.DepartureTime.Date == selectedDate.Date)
        //         .Include(r => r.Buses)
        //         .ToList();

        //     var vm = new List<TripCardViewModel>();

        //     foreach (var route in routes)
        //     {
        //         foreach (var bus in route.Buses)
        //         {
        //             // var remainingSeats = bus.Capacity - route.Tickets.Count(t => t.RouteId == bus.Id);

        //             vm.Add(new TripCardViewModel
        //             {
        //                 RouteId = route.Id,
        //                 Origin = route.Origin,
        //                 Destination = route.Destination,
        //                 Departure = route.DepartureTime,
        //                 Arrival = route.ArrivalTime,
        //                 Price = route.Price,

        //                 BusModel = bus.Model,
        //                 BusNumber = bus.BusNo,

        //                 RemainingSeats = 20
        //             });
        //         }
        //     }

        //     return View("Index", vm);
        // }
    }
}