using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        public IActionResult Search(string origin, string destination, string date)
        {
            DateTime selectedDate = DateTime.Parse(date);

            var routes = context.Routes
                .Include(r => r.Bus) // <<< LOAD BUS DATA
                .Where(r =>
                    r.Origin == origin &&
                    r.Destination == destination &&
                    r.DepartureTime.Date == selectedDate.Date)
                .ToList();

            
            foreach (var route in routes)
            {
                if (route.Bus != null)
                {
                    route.Bus.Routes = null; 
                }
            }
            
            HttpContext.Session.SetString("routes",JsonConvert.SerializeObject(routes));

            return View("Index", routes);
        }
        public IActionResult Filter(List<string>? models, int? minPrice, int? maxPrice, List<TimeSpan>? DepartureTimes)
        {
            

            var json = HttpContext.Session.GetString("routes");
            var routes = JsonConvert.DeserializeObject<List<Route_travel>>(json);

            List<Route_travel> FilteredRoutes = routes != null 
                ? new List<Route_travel>(routes) 
                : new List<Route_travel>();

            if (models != null && models.Count > 0)
            {
                FilteredRoutes = FilteredRoutes
                    .Where(trip => models.Contains(trip.Bus.Model))
                    .ToList();
            }

            if (DepartureTimes != null && DepartureTimes.Count > 0)
            {
                FilteredRoutes = FilteredRoutes
                    .Where(trip => DepartureTimes.Contains(trip.DepartureTime.TimeOfDay))
                    .ToList();
            }

            if (minPrice != null)
            {
                FilteredRoutes = FilteredRoutes
                    .Where(trip => trip.Price >= minPrice)
                    .ToList();
            }

            if (maxPrice != null)
            {
                FilteredRoutes = FilteredRoutes
                    .Where(trip => trip.Price <= maxPrice)
                    .ToList();
            }

            return PartialView("_TripsList", FilteredRoutes);
        }
    }
}