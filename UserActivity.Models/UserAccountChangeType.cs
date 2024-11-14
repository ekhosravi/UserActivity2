using System;
using System.Collections.Generic;

namespace UserActivity.Models;

public partial class UserAccountChangeType
{
    public int Id { get; set; }

    /// <summary>
    /// Name of the change type (e.g., &apos;Profile Update&apos;, &apos;Password Reset&apos;)
    /// </summary>
    public string? ChangeTypeName { get; set; }

    public virtual ICollection<UserAccountChange> UserAccountChanges { get; set; } = new List<UserAccountChange>();
}
