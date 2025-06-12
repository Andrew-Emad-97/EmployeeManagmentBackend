using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.API.DTOs.Employee
{
	public class EmployeeCreateDto
	{
		[Required]
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }

		[Required]
		public string PhoneNumber { get; set; }

		[Required]
		public string NationalId { get; set; }

		[Required]
		public int Age { get; set; }

		public string? Signature { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
