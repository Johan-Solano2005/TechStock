using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechStock.Web.Data;

namespace TechStock.Web.Controllers;

public class ProductsController : Controller
{
    private readonly TechStockContext _context;

    public ProductsController(TechStockContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _context.Products.ToListAsync();
        return View(products);
    }
}