using MVCProject.Infrastructure.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class SignInUser
    {
        [Required, Display(Name = "First Name"), StringLength(50)]
        public string FirstName { get; set; }
        [Required, Display(Name = "Last Name"), StringLength(50)]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}",ApplyFormatInEditMode =true)]
        public DateTime BirthDate { get; set; }
        [Required, StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }
        [Required, Display(Name = "User Name"), StringLength(50)]
        [ValidUserName]
        public string UserName { get; set; }
        [Required, StringLength(50)]
        public string Password { get; set; }
        [Required,Display(Name = "Verification Password"), StringLength(50)]
        [Compare("Password")]
        public string VerificationPassword { get; set; }
    }
}