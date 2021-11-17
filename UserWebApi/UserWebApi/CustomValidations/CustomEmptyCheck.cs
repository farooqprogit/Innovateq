using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using UserWebApi.Models;

namespace UserWebApi.CustomValidations
{
    public class CustomEmptyCheck : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var user = (User)validationContext.ObjectInstance;

            if (string.IsNullOrEmpty(user.Name))
                return new ValidationResult("Name is required.");
            else if (string.IsNullOrEmpty(user.Designation))
                return new ValidationResult("Designation is required.");
            else if (string.IsNullOrEmpty(user.Country))
                return new ValidationResult("Country is required.");
            return ValidationResult.Success;
        }
    }
}
