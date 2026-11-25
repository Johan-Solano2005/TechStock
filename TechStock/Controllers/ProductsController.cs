using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechStock.Models;
using TechStock.Web.Data;

namespace TechStock.Web.Controllers;

public class ProductsController : Controller
{
    private readonly TechStockContext _context;

    public ProductsController(TechStockContext context)
    {
        _context = context;
    }

    // Inactive products are hidden by default: products are deactivated, not deleted, to keep their history
    public async Task<IActionResult> Index(bool showInactive = false)
    {
        // Build the query first; it only hits the database when ToListAsync() runs
        var query = _context.Products.AsQueryable();

        if (!showInactive)
        {
            query = query.Where(p => p.IsActive);
        }

        // Active products first, inactive ones at the bottom
        var products = await query.OrderByDescending(p => p.IsActive).ToListAsync();

        // Lets the view know whether the "Show inactive products" checkbox must be checked
        ViewBag.ShowInactive = showInactive;
        return View(products);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        // Invalid data is not saved; the error is shown on the list page
        if (!ModelState.IsValid)
        {
            SetValidationError();
            return RedirectToAction("Index");
        }

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        // Redirect instead of returning a view, so refreshing the page does not resubmit the form
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Product product)
    {
        if (!ModelState.IsValid)
        {
            SetValidationError();
            return RedirectToAction("Index");
        }

        // Update (not Add): the product arrives with its Id, sent by the hidden field of the edit modal
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    private void SetValidationError()
    {
        var errors = ModelState
            .Where(kv => kv.Value.Errors.Count > 0)
            .Select(kv => $"{kv.Key}: {kv.Value.Errors[0].ErrorMessage}");

        // TempData (not ViewBag) because the message has to survive the redirect
        TempData["Error"] = "Could not save the product. " + string.Join(" | ", errors);
    }
}