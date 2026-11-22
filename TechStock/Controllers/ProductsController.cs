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

    public async Task<IActionResult> Index(bool showInactive = false)
    {
        var query = _context.Products.AsQueryable();

        if (!showInactive)
        {
            query = query.Where(p => p.IsActive);
        }

        var products = await query.OrderByDescending(p => p.IsActive).ToListAsync();

        ViewBag.ShowInactive = showInactive;
        return View(products);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        if (!ModelState.IsValid)
        {
            SetValidationError();
            return RedirectToAction("Index");
        }

        _context.Products.Add(product);
        await _context.SaveChangesAsync();
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

        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    private void SetValidationError()
    {
        var errors = ModelState
            .Where(kv => kv.Value.Errors.Count > 0)
            .Select(kv => $"{kv.Key}: {kv.Value.Errors[0].ErrorMessage}");

        TempData["Error"] = "Could not save the product. " + string.Join(" | ", errors);
    }
}