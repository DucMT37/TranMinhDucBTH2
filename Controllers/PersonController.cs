using TranMinhDucBTH2.Data;
using TranMinhDucBTH2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TranMinhDucBTH2.Controllers
{
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PersonController (ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _context.Person.ToListAsync();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]

        public async Task<IActionResult> Create(Person prs)
        {
            if(ModelState.IsValid)
            {
                _context.Add(prs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prs);
        }
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var person = await _context.Person.FindAsync(id);
            if (Person == null)
            {
                return View("NotFound");
            }
            return View(Person);
        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(string id, [Bind("PersonID,PersonName")] Person prs)
        {
            if (id ! == prs.PersonID)
            {
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(prs.PersonID))
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
            return View(prs);
        
        }

         public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var prs = await _context.Person
                .FirstOrDefaultAsync(m => m.PersonID == id);
            if (prs == null)
            {
                return View("NotFound");
            }

            return View(prs);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var prs = await _context.Person.FindAsync(id);
            _context.Person.Remove(prs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(Person prs)
        {
            if(ModelState.IsValid)
            {
                _context.Add(prs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prs);
        }
        private bool PersonExists(string id)
        {
            return _context.Person.Any(e => e.PersonID == id);
        }
    }
}
