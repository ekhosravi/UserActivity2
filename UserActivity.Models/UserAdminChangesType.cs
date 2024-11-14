using System;
using System.Collections.Generic;

namespace UserActivity.Models;

public partial class UserAdminChangesType
{
    public int Id { get; set; }

    /// <summary>
    /// specifies the type of admin action (e.g., lock,
    /// </summary>
    public string? AdminChangesType { get; set; }

    public virtual ICollection<UserAdminChange> UserAdminChanges { get; set; } = new List<UserAdminChange>();
}
