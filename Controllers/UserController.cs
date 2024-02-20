using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HousingSystem.Data;
using HousingSystem.Models;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using Azure.Identity;


namespace HousingSystem.Controllers;

    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.User.Include(u => u.Occupant);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .Include(u => u.Occupant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            ViewData["OccupantId"] = new SelectList(_context.Occupant, "OccupantId", "OccupantId");
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserRole,UserActive,OccupantId,Id,PasswordHash")] User user)
        {user.UserActive = true;
        if(user.PasswordHash== null){
                    ModelState.AddModelError("PasswordHash", "Password must be at least 8 characters long and include a combination of letters, numbers, and special symbols.");
        return View(user);

        }
         if (!IsValidPassword(user.PasswordHash))
    {
        ModelState.AddModelError("PasswordHash", "Password must be at least 8 characters long and include a combination of letters, numbers, and special symbols.");
        return View(user);
    }

            if (ModelState.IsValid)
            {  
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OccupantId"] = new SelectList(_context.Occupant, "OccupantId", "OccupantId", user.OccupantId);
            return View(user);
        }
        private bool IsValidPassword(string password)
{
    // Implement your custom password validation logic here
    // For example, you can use regular expressions or other criteria
            return Regex.IsMatch(password, @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$");
        }
        // GET: User/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["OccupantId"] = new SelectList(_context.Occupant, "OccupantId", "OccupantId", user.OccupantId);
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserRole,UserActive,OccupantId,Id,PasswordHash")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }
            if(user.PasswordHash== null){
                    ModelState.AddModelError("PasswordHash", "Password must be at least 8 characters long and include a combination of letters, numbers, and special symbols.");
        return View(user);

        }
         if (!IsValidPassword(user.PasswordHash))
    {
        ModelState.AddModelError("PasswordHash", "Password must be at least 8 characters long and include a combination of letters, numbers, and special symbols.");
        return View(user);
    }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["OccupantId"] = new SelectList(_context.Occupant, "OccupantId", "OccupantId", user.OccupantId);
            return View(user);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .Include(u => u.Occupant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(string id)
        {
            if (_context.User == null)
            {
                return Problem("Entity set 'ApplicationDbContext.User'  is null.");
            }
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
          return (_context.User?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    //GET
     public IActionResult Verify()
    {   
       
        return View();
    }
    // POST: User/Verify
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Verify(string occupantId, string passwordHash)
        {
            if (string.IsNullOrEmpty(occupantId) || string.IsNullOrEmpty(passwordHash))
            {
                // Invalid input, return an error view or handle accordingly
                ModelState.AddModelError(string.Empty, "UserName and Password are required.");
                return View();
            }

            // Check if there is a user with the given occupantId and passwordHash
            var user = _context.User.FirstOrDefault(u => u.OccupantId == occupantId && u.PasswordHash == passwordHash);

             if (user != null && user.UserActive)
    {    var occupant = _context.Occupant.FirstOrDefault(o => o.OccupantId == occupantId);

            // Store the occupant name in the session
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            HttpContext.Session.SetString("UserFName", occupant.OccupantFirstName);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            HttpContext.Session.SetString("UserLName", occupant.OccupantLastName);
        
            if (user.UserRole == "Admin")
        {  
            return RedirectToAction("AdminDashboard", "Home");
        }
        else if (user.UserRole == "Chairman")
        {
            return RedirectToAction("ChairmanDashboard", "Home");
        }
        else if (user.UserRole == "Resident")
        {
            return RedirectToAction("ResidentDashboard", "Home");
        }
    }
            else
            {
                // User not found or password doesn't match
                ModelState.AddModelError(string.Empty, "Invalid UserName or Password.");
                return View();
            }
            return View();
            }

             public IActionResult ChangePassword()
    {   
       
        return View();
    }
    // POST: User/Verify
       [HttpPost]
[ValidateAntiForgeryToken]
public IActionResult ChangePassword(string occupantId, string passwordHash, string newpasswordHash)
{   
    if (passwordHash == null || !IsValidPassword(passwordHash))
    {
        ModelState.AddModelError("PasswordHash", "Invalid password format.");
        return View();
    }

    if (!IsValidPassword(newpasswordHash))
    {
        ModelState.AddModelError("NewPassword", "Password must be at least 8 characters long and include a combination of letters, numbers, and special symbols.");
        return View();
    }

    if (ModelState.IsValid)
    {
        // Find the user by occupantId
        var existingUser = _context.User.FirstOrDefault(u => u.OccupantId == occupantId);

        if (existingUser == null || existingUser.PasswordHash != passwordHash)
        {
            ModelState.AddModelError("PasswordHash", "Invalid password.");
            return View();
        }

        // Update the passwordHash with the new password
        existingUser.PasswordHash = newpasswordHash;

        
    
            _context.Update(existingUser);
            _context.SaveChanges();
         TempData["success"]="Password Updated Successfully";
        return RedirectToAction("ResidentDashboard", "Home");

    }

    return View();
}


    }