using System;
using System.Collections.Generic;

namespace UserActivity.Models.Models;

public partial class AspNetUser
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public string? NormalizedUserName { get; set; }

    public string? Email { get; set; }

    public string? NormalizedEmail { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public string Discriminator { get; set; } = null!;

    public int? StatusId { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public DateTime? LastLoginDate { get; set; }


    public virtual Status? Status { get; set; }

    public virtual ICollection<UserAccountChange> UserAccountChangeChangedByUsers { get; set; } = new List<UserAccountChange>();

    public virtual ICollection<UserAccountChange> UserAccountChangeUsers { get; set; } = new List<UserAccountChange>();

    public virtual ICollection<UserAction> UserActions { get; set; } = new List<UserAction>();

    public virtual ICollection<UserAdminChange> UserAdminChangeAdmins { get; set; } = new List<UserAdminChange>();

    public virtual ICollection<UserAdminChange> UserAdminChangeUsers { get; set; } = new List<UserAdminChange>();

    public virtual ICollection<UserApicall> UserApicalls { get; set; } = new List<UserApicall>();

    public virtual ICollection<UserAuditTrail> UserAuditTrails { get; set; } = new List<UserAuditTrail>();

    public virtual ICollection<UserComment> UserComments { get; set; } = new List<UserComment>();


    public virtual ICollection<UserFileInteraction> UserFileInteractions { get; set; } = new List<UserFileInteraction>();


    public virtual ICollection<UserMessage> UserMessageReceivers { get; set; } = new List<UserMessage>();

    public virtual ICollection<UserMessage> UserMessageSenders { get; set; } = new List<UserMessage>();

    public virtual ICollection<UserPageNavigate> UserPageNavigates { get; set; } = new List<UserPageNavigate>();

    public virtual ICollection<UserPasswordReset> UserPasswordResets { get; set; } = new List<UserPasswordReset>();

    public virtual ICollection<UserRoleChange> UserRoleChanges { get; set; } = new List<UserRoleChange>();

    public virtual ICollection<WebPageVisit> WebPageVisits { get; set; } = new List<WebPageVisit>();

    public virtual ICollection<AspNetRole> Roles { get; set; } = new List<AspNetRole>();
}
