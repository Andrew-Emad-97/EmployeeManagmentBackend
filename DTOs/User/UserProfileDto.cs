namespace EmployeeManagement.API.DTOs.User
{
	public class UserProfileDto
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PhoneNumber { get; set; }
		public string NationalId { get; set; }
		public int Age { get; set; }
		public string SignatureUrl { get; set; }
	}
}
