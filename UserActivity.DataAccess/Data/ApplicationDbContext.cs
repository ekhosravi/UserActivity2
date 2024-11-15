
using UserActivity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserActivity.Utility;
using Microsoft.AspNetCore.Identity;

namespace UserActivity.DataAccess;
public class ApplicationDbContext : IdentityDbContext<IdentityUser<int> ,IdentityRole<int> ,int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Status> Status { get; set; }
    public  DbSet<Session> Sessions { get; set; }


    public  DbSet<UserAccountChange> UserAccountChanges { get; set; }

    public DbSet<UserAccountChangeType> UserAccountChangeTypes { get; set; }

    public DbSet<UserAction> UserActions { get; set; }

    public DbSet<UserActionTarget> UserActionTargets { get; set; }

    public DbSet<UserActionType> UserActionTypes { get; set; }

    public DbSet<UserAdminChange> UserAdminChanges { get; set; }

    public DbSet<UserAdminChangesType> UserAdminChangesTypes { get; set; }

    public DbSet<UserApicall> UserApicalls { get; set; }

    public DbSet<UserAuditTrail> UserAuditTrails { get; set; }

    public DbSet<UserAuditTrailType> UserAuditTrailTypes { get; set; }

    public DbSet<UserComment> UserComments { get; set; }

    public DbSet<UserFileInteraction> UserFileInteractions { get; set; }

    public DbSet<UserLogin> UserLogins { get; set; }

    public DbSet<UserMessage> UserMessages { get; set; }

    public DbSet<UserMessagesType> UserMessagesTypes { get; set; }

    public DbSet<UserPageNavigate> UserPageNavigates { get; set; }

    public DbSet<UserPasswordReset> UserPasswordResets { get; set; }

    public DbSet<UserRoleChange> UserRoleChanges { get; set; }

    public DbSet<WebPage> WebPages { get; set; }

    public DbSet<WebPageVisit> WebPageVisits { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed data for Status table
        modelBuilder.Entity<Status>().HasData(
            new Status { StatusId = 1, StatusName = SD.StatusActive  },
            new Status { StatusId = 2, StatusName = SD.StatusInactive},
            new Status { StatusId = 3, StatusName = SD.StatusBanned  }
        );
    }

}

