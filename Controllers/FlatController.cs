using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HousingSystem.Data;
using HousingSystem.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace HousingSystem.Controllers;
public class FlatController : Controller
{
    private readonly ApplicationDbContext _db;
   

    public FlatController(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public IActionResult Index()
    {   var userFName = HttpContext.Session.GetString("UserFName");
       var userLName = HttpContext.Session.GetString("UserLName");
        if(userFName == null || userLName == null ){
        return RedirectToAction("Verify", "User");
       }
        IEnumerable<Flat> objFlatList = _db.Flat;
        return View(objFlatList);
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
    public IActionResult Create(Flat obj)
    {   
        var currentCount = _db.Flat.Count();
        if(currentCount < 22)
    {   bool isUnique = _db.Flat.All(e=>e.FlatNo != obj.FlatNo);

        if(ModelState.IsValid && isUnique) 
    {
       _db.Flat.Add(obj);
       _db.SaveChanges();
       TempData["success"]="Flats are created";
        return RedirectToAction("Index");
    }
    else if (!isUnique){
        ModelState.AddModelError("CustomError", "Give a unique flatno.");
        return View(obj);
    }
    }
    else{
        ModelState.AddModelError("CustomError", "There are only 22 flats you have exceeded the count");
         
    }
   return View(obj);
    }
    
    //GET
    public IActionResult Edit(string? FlatNo)
    {    Console.WriteLine("Edit action is being hit!");
    var userFName = HttpContext.Session.GetString("UserFName");
       var userLName = HttpContext.Session.GetString("UserLName");
        if(userFName == null || userLName == null ){
        return RedirectToAction("Verify", "User");
       }
       if(FlatNo == null){
        Console.WriteLine(FlatNo);
        return NotFound();
       }  
       var FlatFromDb = _db.Flat.Find(FlatNo);
       if(FlatFromDb == null){
        return NotFound();
       }
        return View(FlatFromDb);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Flat obj)
    {  
        if(ModelState.IsValid)
    {
       _db.Flat.Update(obj);
       _db.SaveChanges();
       TempData["success"]="Flats are updated";
        return RedirectToAction("Index");
        
    }
    return View(obj);
    }

    //GET
    public IActionResult Delete(string FlatNo)
    {   var userFName = HttpContext.Session.GetString("UserFName");
       var userLName = HttpContext.Session.GetString("UserLName");
        if(userFName == null || userLName == null ){
        return RedirectToAction("Verify", "User");
       }
       if(FlatNo == null){
        return NotFound();
       }  
       var FlatFromDb = _db.Flat.Find(FlatNo);
       if(FlatFromDb == null){
        return NotFound();
       }
        return View(FlatFromDb);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(string FlatNo)
    {   var obj = _db.Flat.Find(FlatNo);
        if(obj == null){
            return NotFound();
        }
    
       _db.Flat.Remove(obj);
       _db.SaveChanges();
       TempData["success"]="Flats are deleted";
        return RedirectToAction("Index");
    
    
    }
    public IActionResult Details()
    {   
        IEnumerable<Flat> objFlatList = _db.Flat;
        return View(objFlatList);
    }
    


}