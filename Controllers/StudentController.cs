using TranMinhDucBTH2.Data;
using TranMinhDucBTH2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TranMinhDucBTH2.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public StudentController (ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _context.Student.ToListAsync();
            var model = await _context.Employee.ToListAsync();
            var model = await _context.Person.ToListAsync();
            var model = await _context.Customer.ToListAsync();
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return View("NotFound");
            }
            return View(student);

        }


        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var std = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentID == id);
            if (std == null)
            {
                return View("NotFound");
            }

            return View(std);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(string id, [Bind("StudentID,StudentName")] Student std)
        {
            if (id ! == std.StudentID)
            {
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(std);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(std.StudentID))
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
            return View(std);
            
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var std = await _context.Student.FindAsync(id);
            _context.Student.Remove(std);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(Student std)
        {
            if(ModelState.IsValid)
            {
                _context.Add(std);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(std);
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

        [HttpPost]

        public async Task<IActionResult> Create(Person ps)
        {
            if(ModelState.IsValid)
            {
                _context.Add(ps);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ps);
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

        private bool StudentExists(string id)
        {
            return _context.Students.Any(e => e.StudentID == id);
        }
    }
}