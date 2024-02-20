using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HousingSystem.Data;
using HousingSystem.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace HousingSystem.Controllers;
[Controller]
public class ExpenseController : Controller
{
    private readonly ApplicationDbContext _db;
   

    public ExpenseController(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public IActionResult Index()
    {   var userFName = HttpContext.Session.GetString("UserFName");
       var userLName = HttpContext.Session.GetString("UserLName");
        if(userFName == null  || userLName == null ){
        return RedirectToAction("Verify", "User");
       }
        IEnumerable<Expense> objExpenseList = _db.Expense;
        return View(objExpenseList);
    }
    //GET
    public IActionResult Create()
    {   var userFName = HttpContext.Session.GetString("UserFName");
       var userLName = HttpContext.Session.GetString("UserLName");
        if(userFName == null || userLName == null ){
        return RedirectToAction("Verify", "User");
       }
       Expense obj = new Expense
            {
                // Set other properties if needed
                ExpenseProofData = GetDefaultExpenseProofData()
            };
        return View(obj);
    }
    

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
public async Task<IActionResult> Create([Bind("ExpenseId,ExpenseItem,ExpenseDate,ExpenseMonthYear,ExpenseAmount,ExpenseAccountHead,ExpenseRemark,ExpenseProofFile")] Expense obj)
     
    
    { obj.ExpenseProofData = GetDefaultExpenseProofData();
        if (obj.ExpenseProofFile == null || obj.ExpenseProofFile.Length == 0)
    {
        obj.ExpenseProofData = GetDefaultExpenseProofData();
       

    }
        if (!ModelState.IsValid)
    {
        // Log ModelState errors or debug
        var errors = ModelState.Values.SelectMany(v => v.Errors);
        // ... handle errors or debug as needed
        return View(obj);
    }

    // Set default value for ExpenseProofData if ExpenseProofFile is not provided
    if (obj.ExpenseProofFile == null || obj.ExpenseProofFile.Length == 0)
    {
        obj.ExpenseProofData = GetDefaultExpenseProofData();
       

    }
    else
    {
        using (var stream = new MemoryStream())
        {
            await obj.ExpenseProofFile.CopyToAsync(stream);
            obj.ExpenseProofData = stream.ToArray();
        }
    }

    _db.Expense.Add(obj);
    await _db.SaveChangesAsync();

    TempData["success"] = "Expense Is registered";
    return RedirectToAction("Index");
}
private byte[] GetDefaultExpenseProofData()
        {
            // Provide logic to generate or retrieve the default value for ExpenseProofData
            // For example, you can create a default image or set it to a specific value.
            // Here's a simple example assuming a default image is stored in a file:
            string imagePath = "/home/sherlyn/Pictures/website1.png";
            return System.IO.File.ReadAllBytes(imagePath);
        }
   
   
    

    
   

    //GET
    public IActionResult Edit(int ExpenseId)
    {    Console.WriteLine("Edit action is being hit!");
    var userFName = HttpContext.Session.GetString("UserFName");
       var userLName = HttpContext.Session.GetString("UserLName");
        if(userFName == null || userLName == null ){
        return RedirectToAction("Verify", "User");
       }
       if(ExpenseId == 0){
        Console.WriteLine(ExpenseId);
        return NotFound();
       }  
       var ExpenseFromDb = _db.Expense.Find(ExpenseId);
       if(ExpenseFromDb == null){
        return NotFound();
       }
        return View(ExpenseFromDb);
    }

    //POST
    [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(Expense obj)
{       Console.WriteLine("Edit action is being hit!");

    if (ModelState.IsValid)
    {
        // Get the existing Expense object from the database
        var existingExpense = _db.Expense.Find(obj.ExpenseId);

        if (existingExpense == null)
        {
            return NotFound();
        }

        // Check if a new ExpenseProofFile is provided
        if (obj.ExpenseProofFile != null && obj.ExpenseProofFile.Length > 0)
        {
            // Update ExpenseProofData only if a new file is provided
            using (var stream = new MemoryStream())
            {Console.WriteLine("Entered expense proof");
                await obj.ExpenseProofFile.CopyToAsync(stream);
                existingExpense.ExpenseProofData = stream.ToArray();
            }
        }

        // Update other properties
        existingExpense.ExpenseItem = obj.ExpenseItem;
        existingExpense.ExpenseDate = obj.ExpenseDate;
        existingExpense.ExpenseMonthYear = obj.ExpenseMonthYear;
        existingExpense.ExpenseAmount = obj.ExpenseAmount;
        existingExpense.ExpenseAccountHead = obj.ExpenseAccountHead;
        existingExpense.ExpenseRemark = obj.ExpenseRemark;
Console.WriteLine("Yes it is near existing expense");
        // Update the Expense object in the database
        _db.Expense.Update(existingExpense);
        _db.SaveChanges();

        TempData["success"] = "Expense is updated";
        return RedirectToAction("Index");
    }
    Console.WriteLine("No it is near existing expense");

    // ModelState is not valid, return the view with the model
    return View(obj);
}

    //GET
    public IActionResult Delete(int ExpenseId)
    {   var userFName = HttpContext.Session.GetString("UserFName");
       var userLName = HttpContext.Session.GetString("UserLName");
        if(userFName == null || userLName == null ){
        return RedirectToAction("Verify", "User");
       }
       if(ExpenseId == 0){
        return NotFound();
       }  
       var ExpenseFromDb = _db.Expense.Find(ExpenseId);
       if(ExpenseFromDb == null){
        return NotFound();
       }
        return View(ExpenseFromDb);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(int ExpenseId)
    {   var obj = _db.Expense.Find(ExpenseId);
        if(obj == null){
            return NotFound();
        }
    
       _db.Expense.Remove(obj);
       _db.SaveChanges();
       TempData["success"]="Expense is deleted";
        return RedirectToAction("Index");
    
    
    }
    public IActionResult Details()
    {   
        IEnumerable<Expense> objExpenseList = _db.Expense;
        return View(objExpenseList);
    }
    
    
}