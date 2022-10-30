using TranMinhDucBTH2.Data;
using TranMinhDucBTH2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TranMinhDucBTH2.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EmployeeController (ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _context.Employee.ToListAsync();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]

        public async Task<IActionResult> Create(Employee epl)
        {
            if(ModelState.IsValid)
            {
                _context.Add(epl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(epl);
        }
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return View("NotFound");
            }
            return View(employee);
        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(string id, [Bind("EmployeeID,EmployeeName")] Employee epl)
        {
            if (id ! == epl.EmployeeID)
            {
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(epl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(epl.EmployeeID))
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
            return View(epl);
        
        }

         public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var epl = await _context.Employee
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (epl == null)
            {
                return View("NotFound");
            }

            return View(epl);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var epl = await _context.Employee.FindAsync(id);
            _context.Employee.Remove(epl);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(Employee epl)
        {
            if(ModelState.IsValid)
            {
                _context.Add(epl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(epl);
        }
        private bool EmployeeExists(string id)
        {
            return _context.Employee.Any(e => e.EmployeeID == id);
        }
    }
}