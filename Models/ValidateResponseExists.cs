using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WEB2022_ZZFashion.DAL;

namespace WEB2022_ZZFashion.Models
{
    public class ValidateResponseExists : ValidationAttribute
    {
        private ResponseDAL responseContext = new ResponseDAL();
        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            // Get the text to validate
            string text = Convert.ToString(value);
            // Casting the validation context to the "Response" model class
            Response response = (Response)validationContext.ObjectInstance;
            // Get the Response Id from the response instance
            int responseId = response.ResponseID;
            if (responseContext.IsResponseExist(text, responseId))
                // validation failed
                return new ValidationResult
                ("Response already exists!");
            else
                // validation passed 
                return ValidationResult.Success;
        }
        
    }
}
