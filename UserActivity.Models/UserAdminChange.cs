using System;
using System.Collections.Generic;
using UserActivity.Models.Models;

namespace UserActivity.Models;

public partial class UserAdminChange
{
    public int Id { get; set; }

    /// <summary>
    /// References the user whose account was modified
    /// </summary>
    public int? UserId { get; set; }

    public int? AdminId { get; set; }

    /// <summary>
    /// pecifies the type of admin action (e.g., lock
    /// </summary>
    public int? ChangeTypeId { get; set; }

    public string? OldValue { get; set; }

    public string? NewValue { get; set; }

    public virtual AspNetUser? Admin { get; set; }

    public virtual UserAdminChangesType? ChangeType { get; set; }

    public virtual AspNetUser? User { get; set; }
}
