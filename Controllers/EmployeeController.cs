using TranMinhDucBTH2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranMinhDucBTH2.Models.Process;


namespace TranMinhDucBTH2.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        private ExcelProssm _excelProcess = new ExcelProcess();

        public EmployeeController (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Employee.ToListAsync());
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
                            var emp = new Employee();

                            emp.EmpID = dt.Rows[i][0].ToString();
                            emp.EmpName = dt.Rows[i][0].ToString();
                            emp.EmpAddress = dt.Rows[i][0].ToString();

                            _context.Employee.Add(emp);
                        }
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View();
        }
        
        private bool EmployeeExists(string id)
        {
            return _context.Employee.Any(e => e.EmpID == id);
        }

    }
}