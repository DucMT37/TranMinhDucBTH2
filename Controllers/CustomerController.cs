using TranMinhDucBTH2.Data;
using TranMinhDucBTH2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TranMinhDucBTH2.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CustomerController (ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _context.Customer.ToListAsync();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]

        public async Task<IActionResult> Create(Customer cst)
        {
            if(ModelState.IsValid)
            {
                _context.Add(cst);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cst);
        }
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var Customer = await _context.Customer.FindAsync(id);
            if (Customer == null)
            {
                return View("NotFound");
            }
            return View(Customer);
        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(string id, [Bind("CustomerID,CustomerName")] Customer cst)
        {
            if (id ! == cst.CustomerID)
            {
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cst);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(cst.CustomerID))
                    {
                        return View("NotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cst);
        
        }

         public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var cst = await _context.Customer
                .FirstOrDefaultAsync(m => m.CustomerID == id);
            if (cst == null)
            {
                return View("NotFound");
            }

            return View(cst);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cst = await _context.Customer.FindAsync(id);
            _context.Customer.Remove(cst);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(Customer cst)
        {
            if(ModelState.IsValid)
            {
                _context.Add(cst);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(epl);
        }
        private bool CustomerExists(string id)
        {
            return _context.Customer.Any(e => e.CustomerID == id);
        }
    }
}
