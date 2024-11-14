using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace UserActivity.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        [NotMapped]
        public string Role { get; set; }
       
        public int? StatusId { get; set; }
        [ForeignKey("StatusId")]
        [ValidateNever]
        public Status? Status { get; set; }


        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public DateTime LastLoginDate { get; set; } = DateTime.Now;

        public virtual ICollection<UserLogin> UserLogins { get; set; } = new List<UserLogin>();
        public virtual ICollection<UserFailedLogin> UserFailedLogins { get; set; } = new List<UserFailedLogin>();


    }
}
