﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserActivity.Models.Models;

namespace UserActivity.Models;

public partial class UserLogin
{
    [Key]
    public long Id { get; set; }

    public int UserId { get; set; }
    [ForeignKey("Id")]
    [ValidateNever]
    public ApplicationUser? Users { get; set; }

    public DateTime LoginDateTime { get; set; }

    public string? IpAddress { get; set; }

    public string? DevBrowserInfo { get; set; }

}