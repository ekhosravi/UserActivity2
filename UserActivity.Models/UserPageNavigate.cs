using System;
using System.Collections.Generic;

namespace UserActivity.Models;

public partial class UserPageNavigate
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public byte? WebPageId { get; set; }

    public virtual ApplicationUser? User { get; set; }
}
