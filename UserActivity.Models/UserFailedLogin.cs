using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using UserActivity.Models.Models;

namespace UserActivity.Models;

public partial class UserFailedLogin
{
    public string Id { get; set; } = null!;

    public int UserId { get; set; }
    [ForeignKey("Id")]
    [ValidateNever]
    public ApplicationUser? Users { get; set; }


    public DateTime AttemptDateTime { get; set; }

}
