using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HousingSystem.Data;
using HousingSystem.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.RegularExpressions;


namespace HousingSystem.Controllers;
public class NoticenewController : Controller
{
    private readonly ApplicationDbContext _db;
   

    public NoticenewController(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public IActionResult Index()
    {   var userFName = HttpContext.Session.GetString("UserFName");
       var userLName = HttpContext.Session.GetString("UserLName");
        if(userFName == null || userLName == null ){
        return RedirectToAction("Verify", "User");
       }
        IEnumerable<Noticenew> objNoticenewList = _db.Noticenew;
        return View(objNoticenewList);
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
    public IActionResult Create(Noticenew obj)
    {   if(ModelState.IsValid){
        _db.Noticenew.Add(obj);
       _db.SaveChanges();
       TempData["success"]="Notice is created";
        return RedirectToAction("Index");
    }
        
        return View(obj);
    
    }
    
    //GET
    public IActionResult Edit(string NoticeId)
    {    Console.WriteLine("Edit action is being hit!");
    var userFName = HttpContext.Session.GetString("UserFName");
       var userLName = HttpContext.Session.GetString("UserLName");
        if(userFName == null || userLName == null ){
        return RedirectToAction("Verify", "User");
       }
       if(NoticeId == null){
        Console.WriteLine(NoticeId);
        return NotFound();
       }  
       var NoticeFromDb = _db.Noticenew.Find(NoticeId);
       if(NoticeFromDb == null){
        return NotFound();
       }
        return View(NoticeFromDb);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Noticenew obj)
    {  
        if(ModelState.IsValid)
    {
       _db.Noticenew.Update(obj);
       _db.SaveChanges();
       TempData["success"]="Notice is updated";
        return RedirectToAction("Index");
        
    }
    return View(obj);
    }

    //GET
    public IActionResult Delete(string NoticeId)
    {   var userFName = HttpContext.Session.GetString("UserFName");
       var userLName = HttpContext.Session.GetString("UserLName");
        if(userFName == null || userLName == null ){
        return RedirectToAction("Verify", "User");
       }
       if(NoticeId == null){
        return NotFound();
       }  
       var NoticeFromDb = _db.Noticenew.Find(NoticeId);
       if(NoticeFromDb == null){
        return NotFound();
       }
        return View(NoticeFromDb);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(string NoticeId)
    {   var obj = _db.Noticenew.Find(NoticeId);
        if(obj == null){
            return NotFound();
        }
    
       _db.Noticenew.Remove(obj);
       _db.SaveChanges();
       TempData["success"]="Notice is deleted";
        return RedirectToAction("Index");
    
    
    }
    
    


 public IActionResult View(string NoticeId)
    {    Console.WriteLine("Edit action is being hit!");
       if(NoticeId == null){
        Console.WriteLine(NoticeId);
        return NotFound();
       }  
       var NoticeFromDb = _db.Noticenew.Find(NoticeId);
       if(NoticeFromDb == null){
        return NotFound();
       }
        return View(NoticeFromDb);
    }
}