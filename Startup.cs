using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Session;



using HousingSystem.Data;
using Microsoft.EntityFrameworkCore;
using HousingSystem.Models;
using Microsoft.AspNetCore.Identity;








public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
            Configuration.GetConnectionString("DefaultConnection")
        ));
       services.AddScoped<YourJobClass>();
        services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));
        services.AddHostedService<MaintenanceBackgroundService>();
        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        
        services.Configure<IdentityOptions>(options =>
        {
            // Configure your identity options here
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 8;

            // ... (other options)
        });
    services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(30);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });
        services.AddRazorPages().AddRazorRuntimeCompilation();
        
        // Only configure Hangfire if not running in design-time
        
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
      
    // Other configurations

    

    // Other configurations


        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSession();

        // Use Hangfire dashboard only if not running in design-time
        

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }

    // Method to check if the application is running in design-time
   
}
