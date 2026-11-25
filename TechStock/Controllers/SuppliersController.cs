using Microsoft.AspNetCore.Mvc;
using TechStock.Models;
using TechStock.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace TechStock.Controllers
{
    public class SuppliersController : Controller 
    {

        private readonly TechStockContext _context;

        public SuppliersController(TechStockContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var suppliers = await _context.Suppliers.OrderBy(s => s.Name).ToListAsync();
            return View(suppliers);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Supplier supplier)
        {
            // Two suppliers with the same name are most likely a data entry mistake
            bool nameExists = await _context.Suppliers.AnyAsync(s => s.Name == supplier.Name);
            if (nameExists)
            {
                ModelState.AddModelError("Name", "A supplier with this name already exists.");
            }

            // Invalid data is not saved; the error is shown on the list page
            if (!ModelState.IsValid)
            {
                SetValidationError();
                return RedirectToAction("Index");
            }

            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            // Redirect instead of returning a view, so refreshing the page does not resubmit the form
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Supplier supplier)
        {
            // The Id check excludes the supplier being edited, so it does not clash with itself
            bool nameExists = await _context.Suppliers
                .AnyAsync(s => s.Name == supplier.Name && s.Id != supplier.Id);
            if (nameExists)
            {
                ModelState.AddModelError("Name", "A supplier with this name already exists.");
            }

            if (!ModelState.IsValid)
            {
                SetValidationError();
                return RedirectToAction("Index");
            }

            // Update (not Add): the supplier arrives with its Id, sent by the hidden field of the edit modal
            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        private void SetValidationError()
        {
            var errors = ModelState
                .Where(kv => kv.Value.Errors.Count > 0)
                .Select(kv => $"{kv.Key}: {kv.Value.Errors[0].ErrorMessage}");

            // TempData (not ViewBag) because the message has to survive the redirect
            TempData["Error"] = "Could not save the supplier. " + string.Join(" | ", errors);
        }
    }
}
