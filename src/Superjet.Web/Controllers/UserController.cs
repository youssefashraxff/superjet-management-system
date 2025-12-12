using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Superjet.Web.Data;
using Superjet.Web.Models;
namespace Superjet.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext context;
       
        public UserController(AppDbContext context)
        {
            this.context = context;
        }
        public IActionResult Login()
        {
            return View();
        }
    }
}