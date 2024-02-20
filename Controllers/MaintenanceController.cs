using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HousingSystem.Data;
using HousingSystem.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;

namespace HousingSystem.Controllers
{
    public class MaintenanceController : Controller
    {
        
            private readonly IConfiguration _configuration;
            private readonly ApplicationDbContext _db;
   

    public MaintenanceController(ApplicationDbContext db,IConfiguration configuration)
    {_configuration=configuration;
        _db = db;
        
    }
    public IActionResult Index()
{var userFName = HttpContext.Session.GetString("UserFName");
       var userLName = HttpContext.Session.GetString("UserLName");
        if(userFName == null || userLName == null ){
        return RedirectToAction("Verify", "User");
       }
    // Retrieve maintenance data from the database
    var maintenanceData = _db.Maintenance.ToList();  // Adjust this line based on your data retrieval logic

    // Group maintenance data by month and year (assuming MaintenanceMonthYear is a DateTime)
    var groupedData = maintenanceData
        .GroupBy(m => m.MaintenanceMonthYear.ToString("MM/yyyy"))
        .OrderByDescending(g => g.Key)
        .ToList();

    return View(groupedData);
}


    //GET
    public IActionResult Create()
    {   var userFName = HttpContext.Session.GetString("UserFName");
       var userLName = HttpContext.Session.GetString("UserLName");
        if(userFName == null || userLName == null ){
        return RedirectToAction("Verify", "User");
       }
       
        return View();
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Maintenance obj)

    {   
        obj.MaintenanceMonthYear = new DateTime(obj.MaintenanceMonthYear.Year, obj.MaintenanceMonthYear.Month, 1);

         bool isUnique = _db.Maintenance.All(e=>e.MaintenanceId != obj.MaintenanceId);

        if(ModelState.IsValid && isUnique) 
    {    bool flatNoExists = _db.Flat.Any(f => f.FlatNo == obj.FlatNo);
        bool OwnerExists = _db.Occupant.Any(f => f.OccupantId == obj.OccupantId);

        if (!flatNoExists)
        {
            // If the FlatNo doesn't exist, add a model error
            ModelState.AddModelError(nameof(Maintenance.FlatNo), "The provided FlatNo does not exist.");
            return View(obj);
        }
        if(!OwnerExists){
            ModelState.AddModelError(nameof(Maintenance.OccupantId), "The provided OccupantId does not exist.");
            return View(obj);

        }
       _db.Maintenance.Add(obj);
       _db.SaveChanges();
       TempData["success"]="Maintenance Is created";
        return RedirectToAction("Index");
    }
    else if (!isUnique){
        ModelState.AddModelError("CustomError", "Give a unique maintenance id");
        return View(obj);
    }
    return View(obj);
    }
   

    
    
    //GET
    public IActionResult Edit(string MaintenanceId)
    {    Console.WriteLine("Edit action is being hit!");
    var userFName = HttpContext.Session.GetString("UserFName");
       var userLName = HttpContext.Session.GetString("UserLName");
        if(userFName == null || userLName == null ){
        return RedirectToAction("Verify", "User");
       }
       if(MaintenanceId == null){
        Console.WriteLine(MaintenanceId);
        return NotFound();
       }  
       var MaintenanceFromDb = _db.Maintenance.Find(MaintenanceId);
       if(MaintenanceFromDb == null){
        return NotFound();
       }
        return View(MaintenanceFromDb);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Maintenance obj)
    {  
        if(ModelState.IsValid)
    {
       _db.Maintenance.Update(obj);
       _db.SaveChanges();
       TempData["success"]="Maintenance is updated";
        return RedirectToAction("Index");
        
    }
    return View(obj);
    }

    //GET
    public IActionResult Delete(string MaintenanceId)
    {   var userFName = HttpContext.Session.GetString("UserFName");
       var userLName = HttpContext.Session.GetString("UserLName");
        if(userFName == null || userLName == null ){
        return RedirectToAction("Verify", "User");
       }
       if(MaintenanceId == null){
        return NotFound();
       }  
       var MaintenanceFromDb = _db.Maintenance.Find(MaintenanceId);
       if(MaintenanceFromDb == null){
        return NotFound();
       }
        return View(MaintenanceFromDb);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(string MaintenanceId)
    {   var obj = _db.Maintenance.Find(MaintenanceId);
        if(obj == null){
            return NotFound();
        }
    
       _db.Maintenance.Remove(obj);
       _db.SaveChanges();
       TempData["success"]="Maintenance is deleted";
        return RedirectToAction("Index");
    
    
    }
    public IActionResult Details()
    {   var userFName = HttpContext.Session.GetString("UserFName");
       var userLName = HttpContext.Session.GetString("UserLName");
        if(userFName == null || userLName == null ){
        return RedirectToAction("Verify", "User");
       }
       var loggedInOccupantId = _db.Occupant.Where(o=>o.OccupantFirstName == userFName && o.OccupantLastName == userLName)
       .Select(o=>o.OccupantId)
       .FirstOrDefault();

   
       
        List<Maintenance> objMaintenanceList;
         objMaintenanceList = _db.Maintenance
            .Where(m => m.OccupantId == loggedInOccupantId)
            .ToList();
        return View(objMaintenanceList);

    }

    public IActionResult CreatePaymentIntent()
{
    StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

    // Get the currently logged-in user's ID (OccupantId)
    var occupantId = GetOccupantId(); // Implement this method to retrieve the user's OccupantId

    // Get the current month and year
    var currentMonthYear = DateTime.Now.ToString("MM/yyyy");

    // Retrieve the payment amount for the logged-in user and the current month
    var paymentAmountInRupees = _db.Maintenance
        .Where(m => m.OccupantId == occupantId && m.MaintenanceMonthYear.ToString("MM/yyyy") == currentMonthYear)
        .Sum(m => m.MaintenanceAmount);

    var options = new PaymentIntentCreateOptions
    {
        Amount = (long)paymentAmountInRupees * 100, // Convert amount to cents
        Currency = "inr", // Indian Rupees
        // Add any other necessary options
    };

    var service = new PaymentIntentService();
    var paymentIntent = service.Create(options);

    return Json(new { clientSecret = paymentIntent.ClientSecret });
}
public string GetOccupantId()
{
    var userFName = HttpContext.Session.GetString("UserFName");
    var userLName = HttpContext.Session.GetString("UserLName");

    var occupantInfo = _db.Occupant
        .FirstOrDefault(o => o.OccupantFirstName == userFName && o.OccupantLastName == userLName);

    return occupantInfo?.OccupantId;
}

[HttpPost]
public IActionResult CreatePaymentIntent([FromBody] PaymentIntentRequest request)
{
    try
    {
        StripeConfiguration.ApiKey = "sk_test_51OeEKOSIYGdbFdzXa2knShrTFyglGEyFwOqhwb5Xn3F0jt3CTD5rcHNO3kNsFDd52PZVaDTGy2CfmSd4yrpd8c0O00VUTH4rOW";

        // Create a payment intent using Stripe API
        var options = new PaymentIntentCreateOptions
        {
            Amount = (long)request.MaintenanceAmount * 100, // Convert amount to cents
            Currency = "usd",
            Metadata = new Dictionary<string, string>
            {
                { "MaintenanceId", request.MaintenanceId },
                { "MaintenanceAccountHead", request.MaintenanceAccountHead }
            }
        };

        var service = new PaymentIntentService();
        var paymentIntent = service.Create(options);

        return Json(new { clientSecret = paymentIntent.ClientSecret });
    }
    catch (Exception ex)
    {
        // Handle errors appropriately
        return StatusCode(500, ex.Message);
    }
}
public IActionResult PaymentForm(string MaintenanceId, decimal MaintenanceAmount, string MaintenanceAccountHead)
{
    ViewBag.MaintenanceId = MaintenanceId;
    ViewBag.MaintenanceAmount = MaintenanceAmount;
    ViewBag.MaintenanceAccountHead = MaintenanceAccountHead;
    
    return View();
}





   



    


}
}