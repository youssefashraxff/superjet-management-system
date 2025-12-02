using Microsoft.AspNetCore.Mvc;
namespace Superjet.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}