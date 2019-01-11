using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebStoreShared.Models;

namespace MVCProject.Infrastructure.Validators
{
    public class ValidPriceAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            SellingProduct product = (SellingProduct)validationContext.ObjectInstance;
            if (product.Price > 0)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Price must get values bigger than 0!");
            }
        }
    }
}