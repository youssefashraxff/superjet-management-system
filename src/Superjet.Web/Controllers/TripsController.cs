using Microsoft.AspNetCore.Mvc;
namespace Superjet.Web.Controllers
{
    public class TripsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}