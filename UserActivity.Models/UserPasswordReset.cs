using System;
using System.Collections.Generic; 

namespace UserActivity.Models;

public partial class UserPasswordReset
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public DateTime? ResetDate { get; set; }

    public virtual ApplicationUser? User { get; set; }
}
