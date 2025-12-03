using Microsoft.AspNetCore.Mvc;
using Superjet.Web.Data;
namespace Superjet.Web.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext context;
        public HomeController()
        {
            // context = new AppDbContext();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}