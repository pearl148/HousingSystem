using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HousingSystem.Data;
using HousingSystem.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.RegularExpressions;


namespace HousingSystem.Controllers;
public class OccupantController : Controller
{
    private readonly ApplicationDbContext _db;
   

    public OccupantController(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public IActionResult Index()
    {   var userFName = HttpContext.Session.GetString("UserFName");
       var userLName = HttpContext.Session.GetString("UserLName");
        if(userFName == null || userLName == null ){
        return RedirectToAction("Verify", "User");
       }
        IEnumerable<Occupant> objOccupantList = _db.Occupant;
        return View(objOccupantList);
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
    public IActionResult Create(Occupant obj)
    {   var currentCount = _db.Occupant.Count();
        if(currentCount < 22)
    {   bool isUnique = _db.Occupant.All(e=>e.OccupantId != obj.OccupantId);

        if(ModelState.IsValid && isUnique) 
    {    bool flatNoExists = _db.Flat.Any(f => f.FlatNo == obj.FlatNo);
        bool OwnerExists = _db.Owner.Any(f => f.OwnerId == obj.OwnerId);

        if (!flatNoExists)
        {
            // If the FlatNo doesn't exist, add a model error
            ModelState.AddModelError(nameof(Occupant.FlatNo), "The provided FlatNo does not exist.");
            return View(obj);
        }
        if(!OwnerExists){
            ModelState.AddModelError(nameof(Occupant.OwnerId), "The provided OwnerId does not exist.");
            return View(obj);

        }
        if(!Regex.IsMatch(obj.OccupantPhone,@"^[6-9]\d{9}$")){
            ModelState.AddModelError(nameof(Owner.OwnerPhone), "The provided a valid phone number");
            return View(obj);
        }
       _db.Occupant.Add(obj);
       _db.SaveChanges();
       TempData["success"]="Occupant Is created";
        return RedirectToAction("Index");
    }
    else if (!isUnique){
        ModelState.AddModelError("CustomError", "Give a unique occupant id");
        return View(obj);
    }
    }
    else{
        ModelState.AddModelError("CustomError", "There are only 22 flats you have exceeded the count");
         
    }
   return View(obj);
    }
    
    //GET
    public IActionResult Edit(string OccupantId)
    {    Console.WriteLine("Edit action is being hit!");
    var userFName = HttpContext.Session.GetString("UserFName");
       var userLName = HttpContext.Session.GetString("UserLName");
        if(userFName == null || userLName == null ){
        return RedirectToAction("Verify", "User");
       }
       if(OccupantId == null){
        Console.WriteLine(OccupantId);
        return NotFound();
       }  
       var OccupantFromDb = _db.Occupant.Find(OccupantId);
       if(OccupantFromDb == null){
        return NotFound();
       }
        return View(OccupantFromDb);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Occupant obj)
    {  if(!Regex.IsMatch(obj.OccupantPhone,@"^[6-9]\d{9}$")){
            ModelState.AddModelError(nameof(Owner.OwnerPhone), "The provided a valid phone number");
            return View(obj);
        }
        if(ModelState.IsValid)
    {
       _db.Occupant.Update(obj);
       _db.SaveChanges();
       TempData["success"]="Occupant is updated";
        return RedirectToAction("Index");
        
    }
    return View(obj);
    }

    //GET
    public IActionResult Delete(string OccupantId)
    {   var userFName = HttpContext.Session.GetString("UserFName");
       var userLName = HttpContext.Session.GetString("UserLName");
        if(userFName == null || userLName == null ){
        return RedirectToAction("Verify", "User");
       }
       if(OccupantId == null){
        return NotFound();
       }  
       var OccupantFromDb = _db.Occupant.Find(OccupantId);
       if(OccupantFromDb == null){
        return NotFound();
       }
        return View(OccupantFromDb);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(string OccupantId)
    {   var obj = _db.Occupant.Find(OccupantId);
        if(obj == null){
            return NotFound();
        }
    
       _db.Occupant.Remove(obj);
       _db.SaveChanges();
       TempData["success"]="Occupant is deleted";
        return RedirectToAction("Index");
    
    
    }
     public IActionResult Details()
    {   var userFName = HttpContext.Session.GetString("UserFName");
       var userLName = HttpContext.Session.GetString("UserLName");
        if(userFName == null || userLName == null ){
        return RedirectToAction("Verify", "User");
       }
        IEnumerable<Occupant> objOccupantList = _db.Occupant;
        return View(objOccupantList);
    }
    


}