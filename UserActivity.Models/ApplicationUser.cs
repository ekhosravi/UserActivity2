﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace UserActivity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        [ValidateNever]
        public Status? Status { get; set; }


        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public DateTime LastLoginDate { get; set; } = DateTime.Now;


    }
}