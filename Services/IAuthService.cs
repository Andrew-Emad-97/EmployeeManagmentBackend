using EmployeeManagement.API.DTOs;

namespace EmployeeManagement.API.Services
{
	public interface IAuthService
	{
		Task<string> RegisterAsync(RegisterDto registerDto);
		Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
	}
}
