using System;
using System.Collections.Generic;
using UserActivity.Models.Models;

namespace UserActivity.Models;

public partial class UserPageNavigate
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public byte? WebPageId { get; set; }

    public virtual AspNetUser? User { get; set; }
}
