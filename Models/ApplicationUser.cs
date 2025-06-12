using Microsoft.AspNetCore.Identity;

namespace EmployeeManagement.API.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string NationalId { get; set; }
		public int Age { get; set; }
		public string? Signature { get; set; }
		public string? SignaturePath { get; set; }
	}
}
