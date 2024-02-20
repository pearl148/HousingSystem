using Microsoft.AspNetCore.Mvc;
using HousingSystem.Data;
using HousingSystem.Models;
namespace HousingSystem.Controllers;
public class ComplaintController:Controller{
    private readonly ApplicationDbContext _db;

    public ComplaintController(ApplicationDbContext db)
{
    _db = db;
}

public IActionResult Index()
{
     IEnumerable<Complaint> objComplaintList = _db.Complaint;
        return View(objComplaintList);
}

//GET
    public IActionResult Create()
    {   
       var userFName = HttpContext.Session.GetString("UserFName");
       var userLName = HttpContext.Session.GetString("UserLName");
       Console.WriteLine($"UserFName: {userFName}, UserLName: {userLName}");
       if(userFName == null || userLName == null ){
        return RedirectToAction("Verify", "User");
       }
       ViewBag.ComplaintGiver = $"{userFName} {userLName}";

      
       var model = new Complaint
    {
        ComplaintGiver = ViewBag.ComplaintGiver,
        ComplaintId = Guid.NewGuid().ToString()
    };
        return View(model);
    }

     
  [HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Create(Complaint obj)
{   var userFName = HttpContext.Session.GetString("UserFName");
    var userLName = HttpContext.Session.GetString("UserLName");

    ViewBag.ComplaintGiver = $"{userFName} {userLName}";

    // Always set ComplaintGiver
    Console.WriteLine($"ViewBag.ComplaintGiver: {ViewBag.ComplaintGiver}");
obj.ComplaintGiver = ViewBag.ComplaintGiver;

    obj.ComplaintGiver = ViewBag.ComplaintGiver;

    if (ModelState.IsValid && obj.DateOfComplaint >= DateTime.Today.AddDays(-30) && obj.DateOfComplaint <= DateTime.Today)
    {
        obj.ComplaintId = Guid.NewGuid().ToString();
        _db.Complaint.Add(obj);
        _db.SaveChanges();
        TempData["success"] = "Complaint is registered";
        return RedirectToAction("Index");
    }

    // If ModelState is not valid or date conditions are not met, return to the view
    return View(obj);
}

//GET
    public IActionResult Edit(string ComplaintId)
    {    Console.WriteLine("Edit action is being hit!");
        var userFName = HttpContext.Session.GetString("UserFName");
       var userLName = HttpContext.Session.GetString("UserLName");
        if(userFName == null || userLName == null ){
        return RedirectToAction("Verify", "User");
       }
       if(ComplaintId == null){
        Console.WriteLine(ComplaintId);
        return NotFound();
       }  
       var ComplaintFromDb = _db.Complaint.Find(ComplaintId);
       if(ComplaintFromDb == null){
        return NotFound();
       }
        return View(ComplaintFromDb);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Complaint obj)
    {  
        if(ModelState.IsValid)
    {
       _db.Complaint.Update(obj);
       _db.SaveChanges();
       TempData["success"]="Complaint is updated";
        return RedirectToAction("Index");
        
    }
    return View(obj);
    }

    //GET
    public IActionResult Delete(string ComplaintId)
    {   var userFName = HttpContext.Session.GetString("UserFName");
       var userLName = HttpContext.Session.GetString("UserLName");
        if(userFName == null || userLName == null ){
        return RedirectToAction("Verify", "User");
       }
       if(ComplaintId == null){
        return NotFound();
       }  
       var ComplaintFromDb = _db.Complaint.Find(ComplaintId);
       if(ComplaintFromDb == null){
        return NotFound();
       }
        return View(ComplaintFromDb);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(string ComplaintId)
    {   var obj = _db.Complaint.Find(ComplaintId);
        if(obj == null){
            return NotFound();
        }
    
       _db.Complaint.Remove(obj);
       _db.SaveChanges();
       TempData["success"]="Complaint is deleted";
        return RedirectToAction("Index");
    
    
    }
    
    


 public IActionResult View(string ComplaintId)
    {    Console.WriteLine("Edit action is being hit!");
    var userFName = HttpContext.Session.GetString("UserFName");
       var userLName = HttpContext.Session.GetString("UserLName");
        if(userFName == null || userLName == null ){
        return RedirectToAction("Verify", "User");
       }
       if(ComplaintId == null){
        Console.WriteLine(ComplaintId);
        return NotFound();
       }  
       var ComplaintFromDb = _db.Complaint.Find(ComplaintId);
       if(ComplaintFromDb == null){
        return NotFound();
       }
        return View(ComplaintFromDb);
    }
}