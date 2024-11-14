using System;
using System.Collections.Generic;

namespace UserActivity.Models;

public partial class UserActionTarget
{
    public int Id { get; set; }

    /// <summary>
    /// Indicates the target of the action (e.g., &quot;Profile&quot;, &quot;Order&quot;).
    /// </summary>
    public string? ActionTarget { get; set; }

    public virtual ICollection<UserAction> UserActions { get; set; } = new List<UserAction>();
}
