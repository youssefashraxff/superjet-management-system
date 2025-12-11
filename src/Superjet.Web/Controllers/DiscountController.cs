using Microsoft.AspNetCore.Mvc;
using Superjet.Web.Data;
using Superjet.Web.Models;
using Microsoft.EntityFrameworkCore;

public class DiscountController : Controller
{
    private readonly AppDbContext _context;

    public DiscountController(AppDbContext context)
    {
        _context = context;
    }

    // View Discounts
    [HttpGet]
    public IActionResult Index()
    {
        var discounts = _context.Discounts.ToList();  
        return View(discounts);
    }

    // Create Discount
    [HttpPost]
    public IActionResult Create(Discount discount)
    {
        if (ModelState.IsValid)
        {
            _context.Discounts.Add(discount);
            _context.SaveChanges();  
            return RedirectToAction(nameof(Index));
        }
        return View(discount);
    }

    // Update Discount
    [HttpPost]
    public IActionResult Edit(Discount discount)
    {
        if (ModelState.IsValid)
        {
            _context.Discounts.Update(discount);
            _context.SaveChanges(); 
            return RedirectToAction(nameof(Index));
        }
        return View(discount);
    }

    // Delete Discount
    [HttpPost]
    public IActionResult Delete(int id)
    {
        var discount = _context.Discounts.Find(id);  
        if (discount == null) return NotFound();

        _context.Discounts.Remove(discount);
        _context.SaveChanges();  
        return RedirectToAction(nameof(Index));
    }

    // Check if a discount is valid
    public bool IsValid(int discountId)
    {
        var discount = _context.Discounts.Find(discountId);  
        if (discount == null)
        {
            return false;
        }

        if (DateTime.Now >= discount.StartDate && DateTime.Now <= discount.EndDate)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Calculate the final fare using a discount
    public decimal CalculateFinalFare(int discountId, decimal originalFare)
    {
        var discount = _context.Discounts.Find(discountId); 
        if (discount == null)
        {
            return originalFare;
        }

        bool isValid;
        if (DateTime.Now >= discount.StartDate && DateTime.Now <= discount.EndDate)
        {
            isValid = true;
        }
        else
        {
            isValid = false;
        }

        decimal finalFare;
        if (isValid)
        {
            finalFare = originalFare - (originalFare * (decimal)discount.Percentage / 100m);
        }
        else
        {
            finalFare = originalFare;
        }

        return finalFare;
    }
}
