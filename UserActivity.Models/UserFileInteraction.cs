using System;
using System.Collections.Generic;
using UserActivity.Models.Models;

namespace UserActivity.Models;

public partial class UserFileInteraction
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    /// <summary>
    /// Type of file interaction (e.g., &quot;Upload&quot;, &quot;Download&quot;)
    /// </summary>
    public string? FileType { get; set; }

    public string? FilePath { get; set; }

    public DateTime? FileDateTime { get; set; }

    public int? SessionId { get; set; }

    public virtual Session? Session { get; set; }

    public virtual AspNetUser? User { get; set; }
}
