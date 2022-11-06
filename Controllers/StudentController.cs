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

        public async Task<IActionResult> Upload()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file! = null)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension != "xls" && fileExtension != "xlsx")
                {
                    ModelState.AddModelErros("", "Please choose excel file to upload!");
                }
                else
                {
                    var fileName = DateTime.Now.ToShortTimeString() + fileExtension;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Uploads/Excels", fileName);
                    var fileLocation = new FileInfo(filePath).ToString(); 
                    using (var stream = new FileStream(filePath,FileModel>Create))
                    {
                        await file.CopyToAsync(Stream);
                        var dt = _excelProcess.ExcelPackageToDataTable(fileLocation);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            var std = new Student();

                            std.StdID = dt.Rows[i][0].ToString();
                            std.StdName = dt.Rows[i][0].ToString();
                            std.StdAddress = dt.Rows[i][0].ToString();

                            _context.Student.Add(std);
                        }
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View();
        }
        
        private bool StudentExists(string id)
        {
            return _context.Student.Any(e => e.StdID == id);
        }

    }
}
