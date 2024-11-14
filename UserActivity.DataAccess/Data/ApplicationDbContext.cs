
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
    public virtual DbSet<Session> Sessions { get; set; }


    public virtual DbSet<UserAccountChange> UserAccountChanges { get; set; }

    public virtual DbSet<UserAccountChangeType> UserAccountChangeTypes { get; set; }

    public virtual DbSet<UserAction> UserActions { get; set; }

    public virtual DbSet<UserActionTarget> UserActionTargets { get; set; }

    public virtual DbSet<UserActionType> UserActionTypes { get; set; }

    public virtual DbSet<UserAdminChange> UserAdminChanges { get; set; }

    public virtual DbSet<UserAdminChangesType> UserAdminChangesTypes { get; set; }

    public virtual DbSet<UserApicall> UserApicalls { get; set; }

    public virtual DbSet<UserAuditTrail> UserAuditTrails { get; set; }

    public virtual DbSet<UserAuditTrailType> UserAuditTrailTypes { get; set; }

    public virtual DbSet<UserComment> UserComments { get; set; }

    public virtual DbSet<UserFailedLogin> UserFailedLogins { get; set; }

    public virtual DbSet<UserFileInteraction> UserFileInteractions { get; set; }

    public virtual DbSet<UserLogin> UserLogins { get; set; }

    public virtual DbSet<UserMessage> UserMessages { get; set; }

    public virtual DbSet<UserMessagesType> UserMessagesTypes { get; set; }

    public virtual DbSet<UserPageNavigate> UserPageNavigates { get; set; }

    public virtual DbSet<UserPasswordReset> UserPasswordResets { get; set; }

    public virtual DbSet<UserRoleChange> UserRoleChanges { get; set; }

    public virtual DbSet<WebPage> WebPages { get; set; }

    public virtual DbSet<WebPageVisit> WebPageVisits { get; set; }


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

