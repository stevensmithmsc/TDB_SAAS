using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TDB_SAAS.ViewModels
{
    public class DefaultGenderInAllowedGenders : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var titledata = (TitleFormViewModel)validationContext.ObjectInstance;

            if (titledata.DefaultGender == null || titledata.DefaultGender == 0)
            {
                return ValidationResult.Success;
            }

            if (titledata.Genders.Where(t => t.Selected).Select(t => t.GID).Contains((int)titledata.DefaultGender))
            {
                return ValidationResult.Success;
            } else
            {
                return new ValidationResult("Default Gender must be an allowed Gender");
            }
                

            
        }
    }
}