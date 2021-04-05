using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AttendanceSystem.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name ="Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name ="Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class AttendanceViewModel
    {
        public bool IsComing { get; set; }
        public bool IsLeave { get; set; }
    }
}