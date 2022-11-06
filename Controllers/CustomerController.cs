using TranMinhDucBTH2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranMinhDucBTH2.Models.Process;


namespace TranMinhDucBTH2.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        private ExcelProssm _excelProcess = new ExcelProcess();

        public CustomerController (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Customer.ToListAsync());
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
                            var cus = new Customer();

                            cus.CusID = dt.Rows[i][0].ToString();
                            cus.CusName = dt.Rows[i][0].ToString();
                            cus.CusAddress = dt.Rows[i][0].ToString();

                            _context.Customer.Add(cus);
                        }
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View();
        }
        
        private bool CustomerExists(string id)
        {
            return _context.Customer.Any(e => e.CusID == id);
        }

    }
}
