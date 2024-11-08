using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserActivity.Models;

public class Status
{
    [Key]
    public int StatusId { get; set; }
    [Required]
    public string StatusName { get; set; }
}
