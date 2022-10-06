using TranMinhDucBTH2.Data;
using TranMinhDucBTH2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TranMinhDucBTH2.Controllers
{
    public class HController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HController (ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _context.Employee.ToListAsync();
            var model = await _context.Person.ToListAsync();
            var model = await _context.Customer.ToListAsync();
            return View(model);
        }
    }
}