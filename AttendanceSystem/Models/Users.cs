using System;
using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.Models
{
    public class Users
    {
        public int ID { get; set; }

        [Display(Name ="FirstName")]
        [StringLength(30, MinimumLength = 3)]
        public string FirstName { get; set; }

        
        [Display(Name ="LastName")]
        [StringLength(30, MinimumLength = 3)]
        public string LastName { get; set; }

        [Display(Name ="Salary")]
        public decimal Salary { get; set; }

        [Display(Name ="BirthDate")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage ="The email address is required")]
        [Display(Name ="Email")]
        [EmailAddress(ErrorMessage ="Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name ="ConfirmPassword")]
        [Compare("Password",ErrorMessage = "The password and confirm password don't match")]
        public string ConfirmPassword { get; set; }

        [Display(Name ="UserRole")]
        public string UserRole { get; set; }
    }
}