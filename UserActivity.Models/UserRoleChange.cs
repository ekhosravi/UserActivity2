using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic; 

namespace UserActivity.Models;

public partial class UserRoleChange
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? OldRoleId { get; set; }

    public int? NewRoleId { get; set; }

    public DateTime? ChangeDate { get; set; }

    /// <summary>
    /// ecords the user who performed the role change
    /// </summary>
    public string? ChangedByUserId { get; set; }

    public virtual IdentityRole<int>? NewRole { get; set; }

    public virtual IdentityRole<int>? OldRole { get; set; }

    public virtual ApplicationUser? User { get; set; }
}
