using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BlogProject.Models
{
	public class ContactUs
	{
		[Required(ErrorMessage = "Please enter a valid email address.")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set;}

		[Required(ErrorMessage = "Your must provide a PhoneNumber")]
		//[Display(Name = "Home Phone")]
		[DataType(DataType.PhoneNumber)]
		[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
		public string PhoneNumber { get; set; }

		[Required]
		public string Questiontype { get; set; }

		[Required]
		[StringLength(80)]
		public string Text { get; set; }

		[Required]
		public bool EmailYou { get; set; }

		[Required]
		public bool SmsYou { get; set; }
	}
}