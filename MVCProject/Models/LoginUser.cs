using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class LoginUser
    {
        [Required,Display(Name ="User name")]
        public string Name { get; set; }
        public string FirstName { get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsExist { get; set; }
    }
}