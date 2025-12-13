using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Superjet.Web.Data;
using Superjet.Web.Models;

public class UserController : Controller
{
    private readonly AppDbContext _context;

    public UserController(AppDbContext context)
    {
        _context = context;
    }

    // LOGIN
    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    public IActionResult Login(string userName, string password, string? returnUrl)
    {
        var user = _context.Users
            .FirstOrDefault(u => u.UserName == userName && u.Password == password);

        if (user == null)
        {
            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }

        HttpContext.Session.SetInt32("UserId", user.Id);
        HttpContext.Session.SetString("UserName", user.UserName);

        // redirect back to cart / trips if exists
        if (!string.IsNullOrEmpty(returnUrl))
            return Redirect(returnUrl);

        return RedirectToAction("Index", "Trips");
    }

    // REGISTER
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(string userName, string password)
    {
        if (_context.Users.Any(u => u.UserName == userName))
        {
            ModelState.AddModelError("", "Username already exists");
            return View();
        }

        var user = new User
        {
            UserName = userName,
            Password = password
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        // auto login
        HttpContext.Session.SetInt32("UserId", user.Id);
        HttpContext.Session.SetString("UserName", user.UserName);

        return RedirectToAction("Index", "Trips");
    }
    public IActionResult Profile()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login");

        var user = _context.Users
            .Include(u => u.Tickets)
            .ThenInclude(t => t.Route)
            .ThenInclude(r => r.Bus)
            .Include(u => u.Tickets)
            .ThenInclude(t => t.Discount)
            .FirstOrDefault(u => u.Id == userId);

        return View(user);
    }

    // LOGOUT
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Trips");
    }
}