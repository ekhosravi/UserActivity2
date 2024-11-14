using System;
using System.Collections.Generic;
using UserActivity.Models.Models;

namespace UserActivity.Models;

public partial class UserAccountChange
{
    public int Id { get; set; }

    /// <summary>
    /// Links to the user whose account details were changed
    /// </summary>
    public int? UserId { get; set; }

    public int? ChangeTypeId { get; set; }

    public DateTime? ChangeDate { get; set; }

    public string? OldValue { get; set; }

    public string? NewValue { get; set; }

    /// <summary>
    /// Tracks who performed the change
    /// </summary>
    public int? ChangedByUserId { get; set; }

    public virtual UserAccountChangeType? ChangeType { get; set; }

    public virtual AspNetUser? ChangedByUser { get; set; }

    public virtual AspNetUser? User { get; set; }
}
