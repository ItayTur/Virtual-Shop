using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebStoreShared.Data;

namespace MVCProject.Infrastructure.Validators
{
    public class ValidUserNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            SignInUser signModel = (SignInUser)validationContext.ObjectInstance;
            bool Valid = UsersRepository.IsUserNameValid(signModel.UserName);
            if (Valid)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("User name is already in use! choose diffrent");
            }
        }

    }
}