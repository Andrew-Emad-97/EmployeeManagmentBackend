namespace EmployeeManagement.API.DTOs
{
	public class UserDto
	{
		public string Id { get; set; }

		public string FullName { get; set; }

		public string Email { get; set; }

		public string PhoneNumber { get; set; }

		public string NationalId { get; set; }

		public int Age { get; set; }

		public string Role { get; set; }

		public string? Signature { get; set; }
	}
}
