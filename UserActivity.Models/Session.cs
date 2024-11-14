using System;
using System.Collections.Generic;

namespace UserActivity.Models;

public partial class Session
{
    public int Id { get; set; }

    public string? UserId { get; set; }

    public DateTime? SessionStart { get; set; }

    public DateTime? SessionEnd { get; set; }

    public virtual ICollection<UserAction> UserActions { get; set; } = new List<UserAction>();

    public virtual ICollection<UserApicall> UserApicalls { get; set; } = new List<UserApicall>();

    public virtual ICollection<UserComment> UserComments { get; set; } = new List<UserComment>();

    public virtual ICollection<UserFileInteraction> UserFileInteractions { get; set; } = new List<UserFileInteraction>();

    public virtual ICollection<WebPageVisit> WebPageVisits { get; set; } = new List<WebPageVisit>();
}
