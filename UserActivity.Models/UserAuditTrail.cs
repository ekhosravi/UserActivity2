using System;
using System.Collections.Generic;
using UserActivity.Models.Models;

namespace UserActivity.Models;

public partial class UserAuditTrail
{
    public int Id { get; set; }

    /// <summary>
    /// Tracks the ID of the user or admin who performed the action
    /// </summary>
    public int? UserId { get; set; }

    /// <summary>
    ///  indicates the type of event (e.g., Error, Warning)
    /// </summary>
    public int? EventTypeId { get; set; }

    public DateTime? EventDate { get; set; }

    public string? Description { get; set; }

    /// <summary>
    /// Boolean to indicate if the action was performed by an admin (1) or not (0).
    /// </summary>
    public bool? IsAdminAction { get; set; }

    public virtual UserAuditTrailType? EventType { get; set; }

    public virtual AspNetUser? User { get; set; }
}
