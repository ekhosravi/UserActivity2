using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UserActivity.Models.Models;

public partial class UserActivity4Context : DbContext
{
    public UserActivity4Context()
    {
    }

    public UserActivity4Context(DbContextOptions<UserActivity4Context> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

   
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=Khosravi; Database=UserActivity4; Trusted_Connection=True; MultipleActiveResultSets=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.StatusId, "IX_AspNetUsers_StatusId");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Discriminator).HasMaxLength(21);
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasOne(d => d.Status).WithMany(p => p.AspNetUsers).HasForeignKey(d => d.StatusId);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.Property(e => e.SessionEnd).HasColumnType("datetime");
            entity.Property(e => e.SessionStart).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasMaxLength(450);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("Status");
        });

        modelBuilder.Entity<UserAccountChange>(entity =>
        {
            entity.Property(e => e.ChangeDate).HasColumnType("datetime");
            entity.Property(e => e.ChangeTypeId).HasColumnName("ChangeTypeID");
            entity.Property(e => e.ChangedByUserId).HasComment("Tracks who performed the change");
            entity.Property(e => e.UserId)
                .HasComment("Links to the user whose account details were changed")
                .HasColumnName("UserID");

            entity.HasOne(d => d.ChangeType).WithMany(p => p.UserAccountChanges)
                .HasForeignKey(d => d.ChangeTypeId)
                .HasConstraintName("FK_UserAccountChanges_UserChangeTypes");

            entity.HasOne(d => d.ChangedByUser).WithMany(p => p.UserAccountChangeChangedByUsers)
                .HasForeignKey(d => d.ChangedByUserId)
                .HasConstraintName("FK_UserAccountChanges_AspNetUsers1");

            entity.HasOne(d => d.User).WithMany(p => p.UserAccountChangeUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserAccountChanges_AspNetUsers");
        });

        modelBuilder.Entity<UserAccountChangeType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_UserChangeTypes");

            entity.Property(e => e.ChangeTypeName)
                .HasMaxLength(100)
                .HasComment("Name of the change type (e.g., 'Profile Update', 'Password Reset')");
        });

        modelBuilder.Entity<UserAction>(entity =>
        {
            entity.Property(e => e.ActionDateTime).HasColumnType("datetime");
            entity.Property(e => e.ActionDetails)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasComment("Optional details about the action.");
            entity.Property(e => e.ActionTargetId).HasComment("Indicates the target of the action (e.g., \"Profile\", \"Order\").");
            entity.Property(e => e.ActionTypeId).HasComment("Indicates the target of the action (e.g., \"Profile\", \"Order\")");

            entity.HasOne(d => d.ActionTarget).WithMany(p => p.UserActions)
                .HasForeignKey(d => d.ActionTargetId)
                .HasConstraintName("FK_UserActions_UserActionTarget");

            entity.HasOne(d => d.ActionType).WithMany(p => p.UserActions)
                .HasForeignKey(d => d.ActionTypeId)
                .HasConstraintName("FK_UserActions_UserActionType");

            entity.HasOne(d => d.Session).WithMany(p => p.UserActions)
                .HasForeignKey(d => d.SessionId)
                .HasConstraintName("FK_UserActions_Sessions");

            entity.HasOne(d => d.User).WithMany(p => p.UserActions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserActions_AspNetUsers");
        });

        modelBuilder.Entity<UserActionTarget>(entity =>
        {
            entity.ToTable("UserActionTarget");

            entity.Property(e => e.ActionTarget)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasComment("Indicates the target of the action (e.g., \"Profile\", \"Order\").");
        });

        modelBuilder.Entity<UserActionType>(entity =>
        {
            entity.ToTable("UserActionType");

            entity.Property(e => e.ActionType)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasComment("Describes the type of action (e.g., \"Add\", \"Edit\", \"Delete\")");
        });

        modelBuilder.Entity<UserAdminChange>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ChangeTypeId)
                .HasComment("pecifies the type of admin action (e.g., lock")
                .HasColumnName("ChangeTypeID");
            entity.Property(e => e.NewValue).HasMaxLength(20);
            entity.Property(e => e.OldValue).HasMaxLength(20);
            entity.Property(e => e.UserId).HasComment("References the user whose account was modified");

            entity.HasOne(d => d.Admin).WithMany(p => p.UserAdminChangeAdmins)
                .HasForeignKey(d => d.AdminId)
                .HasConstraintName("FK_UserAdminChanges_AspNetUsers1");

            entity.HasOne(d => d.ChangeType).WithMany(p => p.UserAdminChanges)
                .HasForeignKey(d => d.ChangeTypeId)
                .HasConstraintName("FK_UserAdminChanges_UserAdminChangesType");

            entity.HasOne(d => d.User).WithMany(p => p.UserAdminChangeUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserAdminChanges_AspNetUsers");
        });

        modelBuilder.Entity<UserAdminChangesType>(entity =>
        {
            entity.ToTable("UserAdminChangesType");

            entity.Property(e => e.AdminChangesType)
                .HasMaxLength(100)
                .HasComment("specifies the type of admin action (e.g., lock,");
        });

        modelBuilder.Entity<UserApicall>(entity =>
        {
            entity.ToTable("UserAPICalls");

            entity.Property(e => e.CallDateTime).HasColumnType("datetime");
            entity.Property(e => e.EndPoint)
                .HasMaxLength(200)
                .IsFixedLength()
                .HasComment("The API endpoint accessed by the user.");
            entity.Property(e => e.RequestDetails)
                .HasMaxLength(200)
                .IsFixedLength()
                .HasComment("Optional details about the request (e.g., parameters used)");
            entity.Property(e => e.SessionId).HasColumnName("SessionID");

            entity.HasOne(d => d.Session).WithMany(p => p.UserApicalls)
                .HasForeignKey(d => d.SessionId)
                .HasConstraintName("FK_UserAPICalls_Sessions");

            entity.HasOne(d => d.User).WithMany(p => p.UserApicalls)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserAPICalls_AspNetUsers");
        });

        modelBuilder.Entity<UserAuditTrail>(entity =>
        {
            entity.ToTable("UserAuditTrail");

            entity.Property(e => e.EventDate).HasColumnType("datetime");
            entity.Property(e => e.EventTypeId)
                .HasComment(" indicates the type of event (e.g., Error, Warning)")
                .HasColumnName("EventTypeID");
            entity.Property(e => e.IsAdminAction).HasComment("Boolean to indicate if the action was performed by an admin (1) or not (0).");
            entity.Property(e => e.UserId).HasComment("Tracks the ID of the user or admin who performed the action");

            entity.HasOne(d => d.EventType).WithMany(p => p.UserAuditTrails)
                .HasForeignKey(d => d.EventTypeId)
                .HasConstraintName("FK_UserAuditTrail_UserAuditTrailType");

            entity.HasOne(d => d.User).WithMany(p => p.UserAuditTrails)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserAuditTrail_AspNetUsers");
        });

        modelBuilder.Entity<UserAuditTrailType>(entity =>
        {
            entity.ToTable("UserAuditTrailType");

            entity.Property(e => e.EventTypeName)
                .HasMaxLength(100)
                .HasComment("Name of the change type (e.g., 'Profile Update', 'Password Reset')");
        });

        modelBuilder.Entity<UserComment>(entity =>
        {
            entity.Property(e => e.CommentDateTime).HasColumnType("datetime");
            entity.Property(e => e.CommentText).HasComment("The actual text of the comment or review.");
            entity.Property(e => e.SessionId).HasColumnName("SessionID");
            entity.Property(e => e.TargetId)
                .HasComment("Identifier of the entity being commented on (e.g., product ID)")
                .HasColumnName("TargetID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Session).WithMany(p => p.UserComments)
                .HasForeignKey(d => d.SessionId)
                .HasConstraintName("FK_UserComments_Sessions");

            entity.HasOne(d => d.User).WithMany(p => p.UserComments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserComments_AspNetUsers");
        });

        modelBuilder.Entity<UserFailedLogin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_UserFailedLogin");

            entity.HasOne(d => d.User).WithMany(p => p.UserFailedLogins)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserFailedLogins_AspNetUsers");
        });

        modelBuilder.Entity<UserFileInteraction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_UserFileInteraction");

            entity.Property(e => e.FileDateTime).HasColumnType("datetime");
            entity.Property(e => e.FilePath)
                .HasMaxLength(200)
                .IsFixedLength();
            entity.Property(e => e.FileType)
                .HasMaxLength(20)
                .HasComment("Type of file interaction (e.g., \"Upload\", \"Download\")");

            entity.HasOne(d => d.Session).WithMany(p => p.UserFileInteractions)
                .HasForeignKey(d => d.SessionId)
                .HasConstraintName("FK_UserFileInteractions_Sessions");

            entity.HasOne(d => d.User).WithMany(p => p.UserFileInteractions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserFileInteractions_AspNetUsers");
        });

        modelBuilder.Entity<UserLogin>(entity =>
        {
            entity.Property(e => e.DevBrowserInfo)
                .HasMaxLength(100)
                .HasColumnName("Dev_BrowserInfo");
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("IPAddress");
            entity.Property(e => e.LoginDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.UserLogins)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserLogins_AspNetUsers");
        });

        modelBuilder.Entity<UserMessage>(entity =>
        {
            entity.Property(e => e.IsViewed).HasComment("Boolean indicating if the message was read (1) or unread (0)");
            entity.Property(e => e.MsgDate).HasColumnType("datetime");
            entity.Property(e => e.ReceiverId)
                .HasComment("Foreign key to AspNetUsers.Id, identifies the user who received the message.")
                .HasColumnName("ReceiverID");
            entity.Property(e => e.SenderId).HasComment("Foreign key to AspNetUsers.Id, identifies the user who sent the message.");

            entity.HasOne(d => d.MsgType).WithMany(p => p.UserMessages)
                .HasForeignKey(d => d.MsgTypeId)
                .HasConstraintName("FK_UserMessages_UserMessagesType");

            entity.HasOne(d => d.Receiver).WithMany(p => p.UserMessageReceivers)
                .HasForeignKey(d => d.ReceiverId)
                .HasConstraintName("FK_UserMessages_AspNetUsers1");

            entity.HasOne(d => d.Sender).WithMany(p => p.UserMessageSenders)
                .HasForeignKey(d => d.SenderId)
                .HasConstraintName("FK_UserMessages_AspNetUsers");
        });

        modelBuilder.Entity<UserMessagesType>(entity =>
        {
            entity.ToTable("UserMessagesType");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.MsgType)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasComment("Name of the interaction type (e.g., 'Like', 'Follow')");
        });

        modelBuilder.Entity<UserPageNavigate>(entity =>
        {
            entity.ToTable("UserPageNavigate");

            entity.Property(e => e.UserId).HasColumnName("User_Id");
            entity.Property(e => e.WebPageId).HasColumnName("WebPage_Id");

            entity.HasOne(d => d.User).WithMany(p => p.UserPageNavigates)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserPageNavigate_AspNetUsers");
        });

        modelBuilder.Entity<UserPasswordReset>(entity =>
        {
            entity.Property(e => e.ResetDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.UserPasswordResets)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserPasswordResets_AspNetUsers");
        });

        modelBuilder.Entity<UserRoleChange>(entity =>
        {
            entity.Property(e => e.ChangeDate).HasColumnType("datetime");
            entity.Property(e => e.ChangedByUserId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasComment("ecords the user who performed the role change");
            entity.Property(e => e.NewRoleId).HasColumnName("NewRoleID");
            entity.Property(e => e.OldRoleId).HasColumnName("OldRoleID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.NewRole).WithMany(p => p.UserRoleChangeNewRoles)
                .HasForeignKey(d => d.NewRoleId)
                .HasConstraintName("FK_UserRoleChanges_AspNetRoles1");

            entity.HasOne(d => d.OldRole).WithMany(p => p.UserRoleChangeOldRoles)
                .HasForeignKey(d => d.OldRoleId)
                .HasConstraintName("FK_UserRoleChanges_AspNetRoles");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoleChanges)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserRoleChanges_AspNetUsers");
        });

        modelBuilder.Entity<WebPage>(entity =>
        {
            entity.Property(e => e.PageDesc)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.PageName)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.Url)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("URL");
        });

        modelBuilder.Entity<WebPageVisit>(entity =>
        {
            entity.Property(e => e.VisitDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Session).WithMany(p => p.WebPageVisits)
                .HasForeignKey(d => d.SessionId)
                .HasConstraintName("FK_WebPageVisits_Sessions");

            entity.HasOne(d => d.User).WithMany(p => p.WebPageVisits)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_WebPageVisits_AspNetUsers");

            entity.HasOne(d => d.WebPage).WithMany(p => p.WebPageVisits)
                .HasForeignKey(d => d.WebPageId)
                .HasConstraintName("FK_WebPageVisits_WebPages");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
