using EmployeeManagement.API.DTOs;
using EmployeeManagement.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagement.API.Services
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IConfiguration _configuration;
		private readonly RoleManager<IdentityRole> _roleManager;

		public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_configuration = configuration;
			_roleManager = roleManager;
		}





		public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
		{
			var user = await _userManager.FindByEmailAsync(dto.Email);
			if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
				return null;

			var roles = await _userManager.GetRolesAsync(user);

			var claims = new List<Claim>
				{
					new Claim(ClaimTypes.NameIdentifier, user.Id),
					new Claim(ClaimTypes.Email, user.Email),
					new Claim(ClaimTypes.Name, user.UserName),
					new Claim(ClaimTypes.Role, roles.First()) // assuming one role
				};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _configuration["JWT:Issuer"],
				audience: _configuration["JWT:Audience"],
				claims: claims,
				expires: DateTime.Now.AddHours(3),
				signingCredentials: creds
			);

			return new AuthResponseDto
			{
				Token = new JwtSecurityTokenHandler().WriteToken(token),
				Expiration = token.ValidTo,
				Role = roles.First(),
				Email = user.Email,
				UserId = user.Id
			};
		}

		public async Task<string> RegisterAsync(RegisterDto dto)
		{
			var user = new ApplicationUser
			{
				FirstName = dto.FirstName,
				LastName = dto.LastName,
				PhoneNumber = dto.PhoneNumber,
				Email = dto.Email,
				NationalId = dto.NationalId,
				Age = dto.Age,
				Signature = dto.Signature,
				UserName = dto.Email
			};

			var result = await _userManager.CreateAsync(user, dto.Password);

			if (!result.Succeeded)
				return string.Join(", ", result.Errors.Select(e => e.Description));

			// Create role if not exists
			if (!await _roleManager.RoleExistsAsync(dto.Role))
				await _roleManager.CreateAsync(new IdentityRole(dto.Role));

			await _userManager.AddToRoleAsync(user, dto.Role);

			return "User created successfully";
		}
	}
}
