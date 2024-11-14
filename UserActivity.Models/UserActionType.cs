using System;
using System.Collections.Generic;

namespace UserActivity.Models;

public partial class UserActionType
{
    public int Id { get; set; }

    /// <summary>
    /// Describes the type of action (e.g., &quot;Add&quot;, &quot;Edit&quot;, &quot;Delete&quot;)
    /// </summary>
    public string ActionType { get; set; } = null!;

    public virtual ICollection<UserAction> UserActions { get; set; } = new List<UserAction>();
}
