using EmployeeManagement.API.DTOs.User;

namespace EmployeeManagement.API.Services
{
	public interface IUserService
	{
		Task<UserProfileDto> GetProfileAsync(string userId);
		Task<string> UpdateSignatureAsync(string userId, IFormFile file);
	}
}
