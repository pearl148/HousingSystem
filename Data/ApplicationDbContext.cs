
using Microsoft.EntityFrameworkCore;
using HousingSystem.Models;


namespace HousingSystem.Data;
public class ApplicationDbContext:DbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
    {
        
    }
    public DbSet<Flat>Flat{get; set;}
    public DbSet<Owner>Owner{ get; set; }
    public DbSet<Occupant>Occupant{ get; set; }
    public DbSet<User>User{ get; set; }
    
    public DbSet<Payment>Payment{ get; set; }
    public DbSet<Maintenance>Maintenance{ get; set; }
    public DbSet<Expense>Expense{ get; set; }
    public DbSet<Notice>Notice{get; set; }
    
     public DbSet<Noticenew>Noticenew{get; set; }

     public DbSet<Complaint>Complaint{get; set; }
    
        

   protected override void OnModelCreating(ModelBuilder modelBuilder)
{   modelBuilder.Entity<Flat>()
        .HasKey(f => f.FlatNo);

    modelBuilder.Entity<Owner>()
        .HasKey(o => o.OwnerId);

    modelBuilder.Entity<Owner>()
        .HasOne(o => o.Flat)
        .WithOne(f => f.Owner)
        .HasForeignKey<Flat>(f => f.FlatNo)
        .IsRequired(false); 
    modelBuilder.Entity<Occupant>()
                .HasKey(o => o.OccupantId);

            modelBuilder.Entity<Occupant>()
                .HasOne(o => o.Flat)
                .WithOne(f => f.Occupant)
                .HasForeignKey<Occupant>(o => o.FlatNo)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            modelBuilder.Entity<Occupant>()
            .HasOne(o => o.Owner)
             .WithMany(owner => owner.Occupants)
                .HasForeignKey(o => o.OwnerId)
                 .OnDelete(DeleteBehavior.Cascade) // Change to Cascade
                 .IsRequired();
            /*modelBuilder.Entity<Occupant>()
                .HasOne(o => o.Owner)
                .WithMany(owner => owner.Occupants)
                .HasForeignKey(o => o.OwnerId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();*/
                modelBuilder.Entity<Maintenance>()
                .HasKey(o => o.MaintenanceId);
            
            modelBuilder.Entity<Maintenance>()
                .HasOne(o => o.Flat)
                .WithMany(f => f.Maintenance)
                .HasForeignKey(o => o.FlatNo)
                .IsRequired();

            modelBuilder.Entity<Maintenance>()
                .HasOne(o => o.Occupant)
                .WithMany(owner => owner.Maintenances)
                .HasForeignKey(o => o.OccupantId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            modelBuilder.Entity<Payment>()
        .HasOne(p => p.Occupant)
        .WithMany(o => o.Payment)
        .HasForeignKey(p => p.OccupantId);

       


    base.OnModelCreating(modelBuilder);
}

   
}

