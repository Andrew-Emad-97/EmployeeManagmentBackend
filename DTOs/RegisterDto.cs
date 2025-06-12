using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.API.DTOs
{
	public class RegisterDto
	{
		[Required]
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }

		[Required]
		[Phone]
		public string PhoneNumber { get; set; }

		[Required]
		[StringLength(14, MinimumLength = 14)]
		public string NationalId { get; set; }

		[Required]
		[Range(18, 65)]
		public int Age { get; set; }

		public string? Signature { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[MinLength(6)]
		public string Password { get; set; }

		[Required]
		public string Role { get; set; }	
	}
}
