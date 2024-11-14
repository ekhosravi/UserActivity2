using System;
using System.Collections.Generic;
using UserActivity.Models.Models;

namespace UserActivity.Models;

public partial class WebPageVisit
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? SessionId { get; set; }

    public int? WebPageId { get; set; }

    public DateTime? VisitDateTime { get; set; }

    public int? TimeSpent { get; set; }

    public bool? IsEntryPage { get; set; }

    public bool? IsExitPage { get; set; }

    public virtual Session? Session { get; set; }

    public virtual AspNetUser? User { get; set; }

    public virtual WebPage? WebPage { get; set; }
}
