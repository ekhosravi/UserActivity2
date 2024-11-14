using System;
using System.Collections.Generic;

namespace UserActivity.Models.Models;

public partial class AspNetRole
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? NormalizedName { get; set; }

    public string? ConcurrencyStamp { get; set; }


    public virtual ICollection<UserRoleChange> UserRoleChangeNewRoles { get; set; } = new List<UserRoleChange>();

    public virtual ICollection<UserRoleChange> UserRoleChangeOldRoles { get; set; } = new List<UserRoleChange>();

    public virtual ICollection<AspNetUser> Users { get; set; } = new List<AspNetUser>();
}
