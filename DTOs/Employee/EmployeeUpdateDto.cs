namespace EmployeeManagement.API.DTOs.Employee
{
	public class EmployeeUpdateDto
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PhoneNumber { get; set; }
		public string NationalId { get; set; }
		public int Age { get; set; }
		public string? Signature { get; set; }
	}
}
