using Microsoft.AspNetCore.Mvc;
using Superjet.Web.Data;
using Superjet.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Superjet.Web.Controllers
{
    public class DiscountController : Controller
    {
        private readonly AppDbContext _context;

        public DiscountController(AppDbContext context)
        {
            _context = context;
        }

        
        // ADMIN: VIEW ALL DISCOUNTS
        public IActionResult Index()
        {
            var discounts = _context.Discounts.ToList();
            return PartialView(discounts);
        }

        // ADMIN: CREATE DISCOUNT
        [HttpPost]
        public IActionResult Create([FromBody] Discount discount)
        {
            if (discount == null)
                return BadRequest("Invalid discount data");

            _context.Discounts.Add(discount);
            _context.SaveChanges();

            var discounts = _context.Discounts.ToList();
            return PartialView("Index", discounts);
        }

        // ADMIN: DELETE DISCOUNT
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var discount = _context.Discounts.Find(id);
            if (discount == null) return NotFound();

            _context.Discounts.Remove(discount);
            _context.SaveChanges();

            var discounts = _context.Discounts.ToList();
            return PartialView("Index", discounts);
        }

        // USER: CHECK VALIDITY
        public bool IsValid(int discountId)
        {
            var d = _context.Discounts.Find(discountId);
            if (d == null) return false;

            return DateTime.Now >= d.StartDate && DateTime.Now <= d.EndDate;
        }

        // USER: APPLY DISCOUNT
        public decimal CalculateFinalFare(int discountId, decimal originalFare)
        {
            var d = _context.Discounts.Find(discountId);
            if (d == null) return originalFare;

            bool valid = DateTime.Now >= d.StartDate && DateTime.Now <= d.EndDate;

            if (!valid)
                return originalFare;

            return originalFare - (originalFare * d.Percentage / 100m);
        }
    }
}
