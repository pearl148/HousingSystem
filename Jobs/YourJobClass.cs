using System;
using System.Linq;
using HousingSystem.Data; // Add appropriate using statement for your data context
using HousingSystem.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;


public class YourJobClass
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<YourJobClass> _logger;

    public YourJobClass(ApplicationDbContext dbContext, ILogger<YourJobClass> logger)
    {
         _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

   
    public void Execute()
    {
        try{
        _logger.LogInformation("Your Hangfire job is running!");

        // Get the current month and year
        var currentDate = DateTime.UtcNow;
        var currentMonthYear = currentDate.ToString("MM/yyyy");

        // Get all occupants
        var occupants = _dbContext.Occupant.ToList();
        
        // Assuming you have a list of occupants
        _logger.LogInformation($"Number of occupants: {occupants.Count}");
        foreach (var occupant in occupants)
    {_logger.LogInformation("Starting execution of Your Hangfire job...");

// Your existing logic...


    var maintenanceId = GenerateMaintenanceId(occupant.FlatNo, currentDate, "M");
    var sinkingFundId = GenerateMaintenanceId(occupant.FlatNo, currentDate, "SF");
     var existingMaintenance = _dbContext.Maintenance
                .Where(m => m.OccupantId == occupant.OccupantId && m.MaintenanceAccountHead == "Maintenance")
                .OrderByDescending(m => m.MaintenanceMonthYear)
                .FirstOrDefault();
     var existingSinkingFund = _dbContext.Maintenance
                .Where(m => m.OccupantId == occupant.OccupantId && m.MaintenanceAccountHead == "Sinking Fund")
                .OrderByDescending(m => m.MaintenanceMonthYear)
                .FirstOrDefault();
    var lastpayment = existingMaintenance?.MaintenanceAmount; 
    var lastskfund = existingSinkingFund?.MaintenanceAmount;
    if (existingMaintenance != null)
    {
        var payment = _dbContext.Payment
            .FirstOrDefault(p => p.PaymentId == existingMaintenance.MaintenanceId);

        // If payment exists, subtract its amount from existingMaintenance.MaintenanceAmount
        if (payment != null)
        {
            lastpayment = existingMaintenance.MaintenanceAmount - payment.PaymentAmount;
        }
    }
     if (existingSinkingFund != null)
    {
        var payment = _dbContext.Payment
            .FirstOrDefault(p => p.PaymentId == existingSinkingFund.MaintenanceId);

        // If payment exists, subtract its amount from existingMaintenance.MaintenanceAmount
        if (payment != null)
        {
            lastskfund = existingSinkingFund.MaintenanceAmount - payment.PaymentAmount;
        }
    }
    // Maintenance Entry
    var maintenanceEntry = new Maintenance
    {
        MaintenanceId = maintenanceId,
        MaintenanceMonthYear = currentDate,
        MaintenanceAccountHead = "Maintenance",
        MaintenanceAmount = occupant.OccupantIsOwned
            ? lastpayment + 1000 ?? 1000
            : lastpayment + 1100 ?? 1100,
        MaintenanceRemark = $"Automatically generated for {currentMonthYear}",
        FlatNo = occupant.FlatNo,
        OccupantId = occupant.OccupantId
    };
    var occupantsqfeet = _dbContext.Flat
        .FirstOrDefault(p => p.FlatNo == occupant.FlatNo);
    
    // Sinking Fund Entry
   var sinkingFundEntry = new Maintenance
{
    MaintenanceId = sinkingFundId,
    MaintenanceMonthYear = currentDate,
    MaintenanceAccountHead = "Sinking Fund",

    // If existingSinkingFund is null, use 0; otherwise, use its MaintenanceAmount
    MaintenanceAmount = (lastskfund ?? 0) + (occupantsqfeet?.SqFeetArea ?? 0),

    MaintenanceRemark = $"Automatically generated for {currentMonthYear}",
    FlatNo = occupant.FlatNo,
    OccupantId = occupant.OccupantId
};

   
    // Add the entries to the database or perform other operations
    _dbContext.Maintenance.Add(maintenanceEntry);
     _dbContext.Maintenance.Add(sinkingFundEntry);
_logger.LogInformation("Your Hangfire job completed successfully.");

    }
// Save changes to the database
            _dbContext.SaveChanges();
}
 catch (Exception ex)
        {
            _logger.LogError($"An error occurred: {ex.Message}");
            // Log or handle the exception as needed
        }
    }

// Helper function to generate MaintenanceId
private string GenerateMaintenanceId(string flatNo, DateTime currentDate, string type)
{
    string formattedMonthYear = $"{currentDate.Month}-{currentDate.Year % 100}";
    return $"{flatNo}-{formattedMonthYear}-{type}";
}

}