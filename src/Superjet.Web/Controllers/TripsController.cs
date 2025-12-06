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


            return View("Index", routes);
        }
    }
}