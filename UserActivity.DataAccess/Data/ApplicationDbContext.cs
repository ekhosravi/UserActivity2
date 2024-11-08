
using UserActivity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UserActivity.DataAccess;
public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Status> Status { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed data for Status table
        modelBuilder.Entity<Status>().HasData(
            new Status { StatusId = 1, StatusName = "Active" },
            new Status { StatusId = 2, StatusName = "Inactive" },
            new Status { StatusId = 3, StatusName = "Banned" }
        );
    }

}

