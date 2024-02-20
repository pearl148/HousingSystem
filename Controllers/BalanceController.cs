using System;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using HousingSystem.Data;
using HousingSystem.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;


namespace HousingSystem.Controllers; // Make sure to replace YourNamespace.Models with the actual namespace of your Payment model

public class BalanceController : Controller
{
    private readonly ApplicationDbContext _dbContext; // Replace YourDbContext with the actual DbContext class for your application

    public BalanceController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IActionResult BalanceGenerator()
    {var userFName = HttpContext.Session.GetString("UserFName");
       var userLName = HttpContext.Session.GetString("UserLName");
        if(userFName == null || userLName == null ){
        return RedirectToAction("Verify", "User");
       }
        // Fetch payments from the database
        var payments = _dbContext.Payment.ToList(); // Update this query based on your database structure

        return View("BalanceGenerator", payments);
    }

  [HttpPost]
public IActionResult GenerateBalance(string selectedMonthYear)
{Console.WriteLine($"Generating balance for {selectedMonthYear}");

    // ... (rest of the code)

    
    // Parse the selectedMonthYear value to extract month and year
    var selectedDateParts = selectedMonthYear.Split('-');
    if (selectedDateParts.Length == 2 && int.TryParse(selectedDateParts[0], out var selectedMonth) && int.TryParse(selectedDateParts[1], out var selectedYear))
    {
        // Filter payments for the selected month and year with account head 'maintenance'
        var filteredPayments = _dbContext.Payment
            .Where(p => p.PaymentDate.Month == selectedMonth &&
                        p.PaymentDate.Year == selectedYear &&
                        p.PaymentAccountHead == "Maintenance")
            .ToList();

        // Calculate total income (sum of maintenance payments)
        decimal totalIncome = (decimal)filteredPayments.Sum(p => p.PaymentAmount);

        // Fetch expenses for the selected month and year with account head 'maintenance'
        var filteredExpenses = _dbContext.Expense
            .Where(e => e.ExpenseDate.Month == selectedMonth &&
                        e.ExpenseDate.Year == selectedYear &&
                        e.ExpenseAccountHead == "Maintenance")
            .ToList();

        // Calculate total expenses (sum of maintenance expenses)
        decimal totalExpense = (decimal)filteredExpenses.Sum(e => e.ExpenseAmount);

        // Calculate the balance (total income - total expense)
        decimal balance = totalIncome - totalExpense;

        // Create a balance report string
        var balanceReport = $"Balance Report for {CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(selectedMonth)} {selectedYear}:\n\n" +
                            $"Total Income (Maintenance Payments): {filteredPayments.Sum(p => p.PaymentAmount):N}\n\n" +
                            $"\n\nTotal Expense (Maintenance Expenses): {filteredExpenses.Sum(e => e.ExpenseAmount):N}\n\n" +
                             $"Balance: {(filteredPayments.Sum(p => p.PaymentAmount) - filteredExpenses.Sum(e => e.ExpenseAmount)):N}\n\n"+

                            "Payments:\n" +
                            string.Join("\n", filteredPayments.Select(p => $"Occupant ID: {p.OccupantId}, Amount Paid: {p.PaymentAmount:N}")) +
                            "\n\nExpenses:\n" +
                            string.Join("\n", filteredExpenses.Select(e => $"Expense Item: {e.ExpenseItem}, Expense Amount: {e.ExpenseAmount:N}")) ;
                            
                           
        Console.WriteLine($"Balance Report: {balanceReport}");

        // Pass the balance report to the view
        return View("BalanceResult", balanceReport);
    }

    // Redirect back to the BalanceGenerator view if parsing fails
    return RedirectToAction("BalanceGenerator");
}

public IActionResult GenerateSFBalance(string selectedMonthYear)
{Console.WriteLine($"Generating balance for {selectedMonthYear}");

    // ... (rest of the code)

    
    // Parse the selectedMonthYear value to extract month and year
    var selectedDateParts = selectedMonthYear.Split('-');
    if (selectedDateParts.Length == 2 && int.TryParse(selectedDateParts[0], out var selectedMonth) && int.TryParse(selectedDateParts[1], out var selectedYear))
    {
        // Filter payments for the selected month and year with account head 'maintenance'
        var filteredPayments = _dbContext.Payment
            .Where(p => p.PaymentDate.Month == selectedMonth &&
                        p.PaymentDate.Year == selectedYear &&
                        p.PaymentAccountHead == "SinkingFund")
            .ToList();

        // Calculate total income (sum of maintenance payments)
        decimal totalIncome = (decimal)filteredPayments.Sum(p => p.PaymentAmount);

        // Fetch expenses for the selected month and year with account head 'maintenance'
        var filteredExpenses = _dbContext.Expense
            .Where(e => e.ExpenseDate.Month == selectedMonth &&
                        e.ExpenseDate.Year == selectedYear &&
                        e.ExpenseAccountHead == "SinkingFund")
            .ToList();

        // Calculate total expenses (sum of maintenance expenses)
        decimal totalExpense = (decimal)filteredExpenses.Sum(e => e.ExpenseAmount);

        // Calculate the balance (total income - total expense)
        decimal balance = totalIncome - totalExpense;

        // Create a balance report string
        var balanceReport = $"Balance Report for {CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(selectedMonth)} {selectedYear}:\n\n" +
                            $"Total Income (SF Payments): {filteredPayments.Sum(p => p.PaymentAmount):N}\n\n" +
                            $"\n\nTotal Expense (SF Expenses): {filteredExpenses.Sum(e => e.ExpenseAmount):N}\n\n" +
                             $"Balance: {(filteredPayments.Sum(p => p.PaymentAmount) - filteredExpenses.Sum(e => e.ExpenseAmount)):N}\n\n"+

                            "Payments:\n" +
                            string.Join("\n", filteredPayments.Select(p => $"Occupant ID: {p.OccupantId}, Amount Paid: {p.PaymentAmount:N}")) +
                            "\n\nExpenses:\n" +
                            string.Join("\n", filteredExpenses.Select(e => $"Expense Item: {e.ExpenseItem}, Expense Amount: {e.ExpenseAmount:N}")) ;
                            
                           
        Console.WriteLine($"Balance Report: {balanceReport}");

        // Pass the balance report to the view
        return View("BalanceResult", balanceReport);
    }

    // Redirect back to the BalanceGenerator view if parsing fails
    return RedirectToAction("BalanceGenerator");
}


}
