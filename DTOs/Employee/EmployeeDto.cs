namespace EmployeeManagement.API.DTOs.Employee
{
	public class EmployeeDto
	{
		public string Id { get; set; } // Identity User Id
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PhoneNumber { get; set; }
		public string NationalId { get; set; }
		public int Age { get; set; }
		public string? Signature { get; set; }
		public string Email { get; set; }
	}
}
