using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HousingSystem.Data;
using HousingSystem.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Stripe;
using Microsoft.Extensions.Configuration;


namespace HousingSystem.Controllers;
public class PaymentController : Controller
{
   
    private readonly ApplicationDbContext _db;
   

    public PaymentController(ApplicationDbContext db)
    {
        _db = db;
    }
     
     
   public IActionResult Index()
{var userFName = HttpContext.Session.GetString("UserFName");
       var userLName = HttpContext.Session.GetString("UserLName");
        if(userFName == null || userLName == null ){
        return RedirectToAction("Verify", "User");
       }
    // Retrieve maintenance data from the database
    var PaymentData = _db.Payment.ToList();  // Adjust this line based on your data retrieval logic

    // Group maintenance data by month and year (assuming MaintenanceMonthYear is a DateTime)
    var groupedData = PaymentData
        .GroupBy(m => m.PaymentDate.ToString("MM/yyyy"))
        .OrderByDescending(g => g.Key)
        .ToList();

    return View(groupedData);
}

    //GET
    public IActionResult Create()
    {   
       var userFName = HttpContext.Session.GetString("UserFName");
       var userLName = HttpContext.Session.GetString("UserLName");
        if(userFName == null || userLName == null ){
        return RedirectToAction("Verify", "User");
       }
        return View();
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Payment obj)

    {   
        bool OccupantExists =_db.Occupant.Any(f => f.OccupantId == obj.OccupantId);
         bool isUnique = _db.Payment.All(e=>e.PaymentId != obj.PaymentId);

        if(ModelState.IsValid && isUnique) 
    {   
        bool PaymentReceiptIdExists = _db.Payment.Any(f => f.PaymentReceiptId == obj.PaymentReceiptId);
        bool PaymentTransactionIdExists = _db.Payment.Any(f => f.PaymentTransactionId == obj.PaymentTransactionId);
        if(!OccupantExists){
        ModelState.AddModelError("CustomError", "Provided occupantId doesnt exist ");
        View(obj);
        }
        else if(obj.PaymentTransactionId == "_" ||!PaymentTransactionIdExists &&obj.PaymentReceiptId=="_"||!PaymentReceiptIdExists){
            if(obj.PaymentReceiptId == "_"){
                obj.PaymentReceiptDate=new DateTime(1, 1, 1);
            }
             _db.Payment.Add(obj);
                _db.SaveChanges();
                TempData["success"]="Payment Is created";
                return RedirectToAction("Index");
        }
        else if(PaymentReceiptIdExists){
        ModelState.AddModelError("CustomError", "Give a unique payment  receipt id");

        }
        else if(PaymentTransactionIdExists){
        ModelState.AddModelError("CustomError", "Give a unique payment transaction id");

        }
        
        
       
    }
    else if (!isUnique){
        ModelState.AddModelError("CustomError", "Give a unique payment id");
        return View(obj);
    }
    return View(obj);
    }
   

    
    
    //GET
    public IActionResult Edit(string PaymentId)
    {   var userFName = HttpContext.Session.GetString("UserFName");
       var userLName = HttpContext.Session.GetString("UserLName");
        if(userFName == null || userLName == null ){
        return RedirectToAction("Verify", "User");
       }
         Console.WriteLine("Edit action is being hit!");
       if(PaymentId == null){
        Console.WriteLine(PaymentId);
        return NotFound();
       }  
       var PaymentFromDb = _db.Payment.Find(PaymentId);
       if(PaymentFromDb == null){
        return NotFound();
       }
        return View(PaymentFromDb);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Payment obj)
    {  
        if(ModelState.IsValid)
    {
       _db.Payment.Update(obj);
       _db.SaveChanges();
       TempData["success"]="Payment is updated";
        return RedirectToAction("Index");
        
    }
    return View(obj);
    }

    //GET
    public IActionResult Delete(string PaymentId)
    {   var userFName = HttpContext.Session.GetString("UserFName");
       var userLName = HttpContext.Session.GetString("UserLName");
        if(userFName == null || userLName == null ){
        return RedirectToAction("Verify", "User");
       }
       if(PaymentId == null){
        return NotFound();
       }  
       var PaymentFromDb = _db.Payment.Find(PaymentId);
       if(PaymentFromDb == null){
        return NotFound();
       }
        return View(PaymentFromDb);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(string PaymentId)
    {   var obj = _db.Payment.Find(PaymentId);
        if(obj == null){
            return NotFound();
        }
    
       _db.Payment.Remove(obj);
       _db.SaveChanges();
       TempData["success"]="Payment is deleted";
        return RedirectToAction("Index");
    
    
    }
     public IActionResult Details()
    {   var userFName = HttpContext.Session.GetString("UserFName");
       var userLName = HttpContext.Session.GetString("UserLName");
        if(userFName == null || userLName == null ){
        return RedirectToAction("Verify", "User");
       }
         var loggedInOccupantId = _db.User
        .Where(u => u.UserName == User.Identity.Name)
        .Select(u => u.OccupantId)
        .FirstOrDefault();
        List<Payment> objPaymentList;
         objPaymentList = _db.Payment
            .Where(m => m.OccupantId == loggedInOccupantId)
            .ToList();
        return View(objPaymentList);
    }
    public IActionResult BalanceGeneration(){
        return View();
    }
    


}