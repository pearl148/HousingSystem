using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HousingSystem.Data;
using HousingSystem.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.RegularExpressions;


namespace HousingSystem.Controllers;
public class OwnerController : Controller
{
    private readonly ApplicationDbContext _db;
   

    public OwnerController(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public IActionResult Index()
    {   var userFName = HttpContext.Session.GetString("UserFName");
       var userLName = HttpContext.Session.GetString("UserLName");
        if(userFName == null || userLName == null ){
        return RedirectToAction("Verify", "User");
       }
        IEnumerable<Owner> objOwnerList = _db.Owner;
        return View(objOwnerList);
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
    public IActionResult Create(Owner obj)
    {   var currentCount = _db.Owner.Count();
        if(currentCount < 22)
    {   bool isUnique = _db.Owner.All(e=>e.OwnerId != obj.OwnerId);

        if(ModelState.IsValid && isUnique) 
    {    bool flatNoExists = _db.Flat.Any(f => f.FlatNo == obj.FlatNo);

        if (!flatNoExists)
        {
            // If the FlatNo doesn't exist, add a model error
            ModelState.AddModelError(nameof(Owner.FlatNo), "The provided FlatNo does not exist.");
            return View(obj);
        }
        if(!Regex.IsMatch(obj.OwnerPhone,@"^[6-9]\d{9}$")){
            ModelState.AddModelError(nameof(Owner.OwnerPhone), "The provided a valid phone number");
            return View(obj);
        }
        
       _db.Owner.Add(obj);
       _db.SaveChanges();
       TempData["success"]="Owner Is created";
        return RedirectToAction("Index");
    }
    else if (!isUnique){
        ModelState.AddModelError("CustomError", "Give a unique owner id");
        return View(obj);
    }
    }
    else{
        ModelState.AddModelError("CustomError", "There are only 22 flats you have exceeded the count");
         
    }
   return View(obj);
    }
    
    //GET
    public IActionResult Edit(string? OwnerId)
    {    Console.WriteLine("Edit action is being hit!");
    var userFName = HttpContext.Session.GetString("UserFName");
       var userLName = HttpContext.Session.GetString("UserLName");
        if(userFName == null || userLName == null ){
        return RedirectToAction("Verify", "User");
       }
       if(OwnerId == null){
        Console.WriteLine(OwnerId);
        return NotFound();
       }  
       var OwnerFromDb = _db.Owner.Find(OwnerId);
       if(OwnerFromDb == null){
        return NotFound();
       }
       
        return View(OwnerFromDb);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Owner obj)
    {   if(!Regex.IsMatch(obj.OwnerPhone,@"^[6-9]\d{9}$")){
            ModelState.AddModelError(nameof(Owner.OwnerPhone), "The provided a valid phone number");
            return View(obj);
        }
        
        if(ModelState.IsValid)
    {
       _db.Owner.Update(obj);
       _db.SaveChanges();
       TempData["success"]="Owner is updated";
        return RedirectToAction("Index");
        
    }
    return View(obj);
    }

    //GET
    public IActionResult Delete(string OwnerId)
    {   var userFName = HttpContext.Session.GetString("UserFName");
       var userLName = HttpContext.Session.GetString("UserLName");
        if(userFName == null || userLName == null ){
        return RedirectToAction("Verify", "User");
       }
       if(OwnerId == null){
        return NotFound();
       }  
       var OwnerFromDb = _db.Owner.Find(OwnerId);
       if(OwnerFromDb == null){
        return NotFound();
       }
        return View(OwnerFromDb);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(string OwnerId)
    {   var obj = _db.Owner.Find(OwnerId);
        if(obj == null){
            return NotFound();
        }
    
       _db.Owner.Remove(obj);
       _db.SaveChanges();
       TempData["success"]="Owner is deleted";
        return RedirectToAction("Index");
    
    
    }
   public IActionResult Details()
    {   var userFName = HttpContext.Session.GetString("UserFName");
       var userLName = HttpContext.Session.GetString("UserLName");
        if(userFName == null || userLName == null ){
        return RedirectToAction("Verify", "User");
       }
        IEnumerable<Owner> objOwnerList = _db.Owner;
        return View(objOwnerList);
    }
    


}