using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HousingSystem.Models;


namespace HousingSystem.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
     public IActionResult AdminDashboard()
    {
        // Logic for Admin Dashboard
        return View();
    }

    public IActionResult ChairmanDashboard()
    {
        // Logic for Chairman Dashboard
        return View();
    }

    public IActionResult ResidentDashboard()
    {
        // Logic for Resident Dashboard
        return View();
    }

   
}
