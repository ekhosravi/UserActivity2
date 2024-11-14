using System;
using System.Collections.Generic;

namespace UserActivity.Models;

public partial class WebPage
{
    public int Id { get; set; }

    public string? PageName { get; set; }

    public string? Url { get; set; }

    public string? PageDesc { get; set; }

    public virtual ICollection<WebPageVisit> WebPageVisits { get; set; } = new List<WebPageVisit>();
}
