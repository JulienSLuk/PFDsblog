using System;
using System.ComponentModel.DataAnnotations;
using WEB2022_ZZFashion.DAL;
namespace WEB2022_ZZFashion.Models
{
	public class ValidateEmailExists : ValidationAttribute
	{
		private CustomerDAL customerContext = new CustomerDAL();
		protected override ValidationResult IsValid(
		object value, ValidationContext validationContext)
		{
			// Get the email value to validate
			string email = Convert.ToString(value);
			// Casting the validation context to the "Customer" model class
			Customer customer = (Customer)validationContext.ObjectInstance;
			// Get the Customer Id from the Customer instance
			string memberID = customer.MemberID;
			if (customerContext.IsEmailExist(email, memberID))
			{
				// validation failed
				return new ValidationResult
				("Email address already exists!");
			}

			else
			{
				// validation passed
				return ValidationResult.Success;
			}

		}
	}
}
