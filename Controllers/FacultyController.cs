using TranMinhDucBTH2.Data;
using TranMinhDucBTH2.Models.Process;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranMinhDucBTH2.Models;

namespace TranMinhDucBTH2.Controllers;

public class FacultyController : Controller
{
  private readonly ApplicationDbContext _context;
  private StringProcess _stringProcess = new StringProcess();

  public FacultyController(ApplicationDbContext context)
  {
    _context = context;
  }
  public async Task<IActionResult> Index()
  {
    var model = await _context.Faculties.ToListAsync();
    return View(model);
  }
  [HttpGet]
  public IActionResult Create()
  {
    var newFacultyID = "FCL001";
    var lastFaculty = _context.Faculties.OrderByDescending(f => f.FacultyID).FirstOrDefault();
    if (lastFaculty != null)
    {
      newFacultyID = _stringProcess.AutoGenerateCode(lastFaculty.FacultyID);
    }
    ViewData["FacultyID"] = newFacultyID;

    return View();
  }
  [HttpPost]
  public IActionResult Create(Faculty faculty)
  {
    if (ModelState.IsValid)
    {
      _context.Faculties.Add(faculty);
      _context.SaveChanges();
      return RedirectToAction(nameof(Index));
    }
    return View(faculty);
  }
}