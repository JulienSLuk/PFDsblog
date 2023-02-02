using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WEB2022_ZZFashion.Models
{
	public class TempCustomer
	{
		public int MemberID { get; set; }


		[Required]
		[Display(Name = "Name*")]
		[StringLength(50, ErrorMessage =
		"Name cannot exceed 50 characters")]
		public string Name { get; set; }


		[Required(ErrorMessage =
		"Gender must be filled")]
		[Display(Name = "Gender*")]
		[RegularExpression(@"[M]|[F]$",
			ErrorMessage = "Invalid gender")]
		public char Gender { get; set; }


		[Required(ErrorMessage =
			"Date of Birth must be filled")]
		[Display(Name = "Date of Birth*")]
		[DataType(DataType.Date)]
		public DateTime DOB { get; set; }


		[Display(Name = "Residential Address")]
		[StringLength(255, ErrorMessage =
		"Name cannot exceed 255 characters")]
		public string? ResidentialAddr { get; set; }


		[Required(ErrorMessage =
			"Country of Residence must be filled")]
		[Display(Name = "Country of Residence*")]
		[StringLength(50, ErrorMessage =
		"Country cannot exceed 50 characters")]
		public string Country { get; set; }

		[Display(Name = "Contact Number")]
		[StringLength(50, ErrorMessage =
		"Contact number cannot exceed 20 characters")]
		
		public string ContactNo { get; set; }


		[Display(Name = "Email Address")]
		[StringLength(50, ErrorMessage =
		"Name cannot exceed 50 characters")]
		[EmailAddress(ErrorMessage = "Invalid Email Address")]
		
		public string? Email { get; set; }


		[Required]
		[StringLength(20, ErrorMessage =
		"Password cannot exceed 20 characters")]
		public string Password { get; set; }
	}
}
