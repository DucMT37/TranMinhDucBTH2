using TranMinhDucBTH2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranMinhDucBTH2.Models.Process;


namespace TranMinhDucBTH2.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        private ExcelProssm _excelProcess = new ExcelProcess();

        public StudentController (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Student.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["Faculty"] = new SelectList(_context.Faculty,"FacultyID", "FacultyName");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("StudentID,StudentName,FacultyID")] Student student)
        {
            if(ModelState.IsValid)
            {
                _context.Add(cst);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacultyID" = new SelectList(_context.Faculty,"FacultyID", "FacultyName", student.FacultyID)]
            return View(student);
        }
    }    
}