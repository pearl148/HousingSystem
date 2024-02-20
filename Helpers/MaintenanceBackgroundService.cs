using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class MaintenanceBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<MaintenanceBackgroundService> _logger;

    public MaintenanceBackgroundService(IServiceProvider serviceProvider, ILogger<MaintenanceBackgroundService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                // Your job logic
                using (var scope = _serviceProvider.CreateScope())
                {
                    var jobClass = scope.ServiceProvider.GetRequiredService<YourJobClass>();
                    jobClass.Execute();
                }

                _logger.LogInformation("Maintenance job executed successfully.");

                // Sleep for a specific duration before running the job again
                await Task.Delay(TimeSpan.FromDays(30), stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex.Message}");
                // Log or handle the exception as needed
            }
        }
    }
}
