using System;
using System.Collections.Generic;

namespace UserActivity.Models;

public partial class UserAuditTrailType
{
    public int Id { get; set; }

    /// <summary>
    /// Name of the change type (e.g., &apos;Profile Update&apos;, &apos;Password Reset&apos;)
    /// </summary>
    public string? EventTypeName { get; set; }

    public virtual ICollection<UserAuditTrail> UserAuditTrails { get; set; } = new List<UserAuditTrail>();
}
