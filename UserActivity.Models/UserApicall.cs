using System;
using System.Collections.Generic;

namespace UserActivity.Models;

public partial class UserApicall
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    /// <summary>
    /// The API endpoint accessed by the user.
    /// </summary>
    public string? EndPoint { get; set; }

    public DateTime? CallDateTime { get; set; }

    /// <summary>
    /// Optional details about the request (e.g., parameters used)
    /// </summary>
    public string? RequestDetails { get; set; }

    public int? SessionId { get; set; }

    public virtual Session? Session { get; set; }

    public virtual ApplicationUser? User { get; set; }
}
