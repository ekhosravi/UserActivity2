using System;
using System.Collections.Generic; 

namespace UserActivity.Models;

public partial class UserAction
{
    public int Id { get; set; }

    public int UserId { get; set; }

    /// <summary>
    /// Indicates the target of the action (e.g., &quot;Profile&quot;, &quot;Order&quot;)
    /// </summary>
    public int? ActionTypeId { get; set; }

    /// <summary>
    /// Indicates the target of the action (e.g., &quot;Profile&quot;, &quot;Order&quot;).
    /// </summary>
    public int? ActionTargetId { get; set; }

    /// <summary>
    /// Optional details about the action.
    /// </summary>
    public string? ActionDetails { get; set; }

    public DateTime? ActionDateTime { get; set; }

    public int? SessionId { get; set; }

    public virtual UserActionTarget? ActionTarget { get; set; }

    public virtual UserActionType? ActionType { get; set; }

    public virtual Session? Session { get; set; }

    public virtual ApplicationUser User { get; set; } = null!;
}
