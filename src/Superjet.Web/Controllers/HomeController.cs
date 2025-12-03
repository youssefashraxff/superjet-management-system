using Microsoft.AspNetCore.Mvc;
using Superjet.Web.Data;
namespace Superjet.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext context;
        public HomeController(AppDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var origins = this.context.BusRoutes.Select(o => o.Origin).Distinct().ToList();
            var destinations = this.context.BusRoutes.Select(o => o.Destination).Distinct().ToList();

            var viewModel = new HomeViewModel
            {
                Origins = origins,
                Destinations = destinations
            };

            return View(viewModel);
        }
    }
}