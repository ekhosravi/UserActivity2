using System;
using System.Collections.Generic;
using UserActivity.Models.Models;

namespace UserActivity.Models;

public partial class UserComment
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    /// <summary>
    /// Identifier of the entity being commented on (e.g., product ID)
    /// </summary>
    public int? TargetId { get; set; }

    /// <summary>
    /// The actual text of the comment or review.
    /// </summary>
    public string? CommentText { get; set; }

    public DateTime? CommentDateTime { get; set; }

    public int? SessionId { get; set; }

    public virtual Session? Session { get; set; }

    public virtual AspNetUser? User { get; set; }
}
