 using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HousingSystem.Data;
using HousingSystem.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Stimulsoft.Report;



using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Mvc;
using Stimulsoft.Report.Web;


namespace HousingSystem.Controllers;
public class ReportCollectionController : Controller
{
    private readonly ApplicationDbContext _db;
   

    public ReportCollectionController(ApplicationDbContext db)
    {
        _db = db;
    }
 public IActionResult Index()
    {   var userFName = HttpContext.Session.GetString("UserFName");
       var userLName = HttpContext.Session.GetString("UserLName");
        if(userFName == null || userLName == null ){
        return RedirectToAction("Verify", "User");
       }
       
        return View();
    }
     public IActionResult ExpenseDateType()
    {   
      
        return View();
    }
 [HttpPost]
public IActionResult GenerateReport(DateTime startDate, DateTime endDate, [FromServices] IWebHostEnvironment hostingEnvironment)
{
    // Load your Stimulsoft report file
    string reportPath = Path.Combine(hostingEnvironment.WebRootPath, "ReportCollection", "ExpenseTypeandDateReport.mrt");
    StiReport report = new StiReport();
    report.Load(reportPath);

    // Set the variables in the report
    report.Dictionary.Variables["startDate"].ValueObject = startDate;
    report.Dictionary.Variables["endDate"].ValueObject = endDate;

    // Export the report directly to a PDF file format
    using (MemoryStream ms = new MemoryStream())
    {
        report.ExportDocument(StiExportFormat.Pdf, ms);
        return File(ms.ToArray(), "application/pdf", "ExpenseTypeandDateReport.pdf");
    }
}





}