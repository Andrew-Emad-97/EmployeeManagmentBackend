using EmployeeManagement.API.DTOs.User;
using EmployeeManagement.API.Models;
using Microsoft.AspNetCore.Identity;

namespace EmployeeManagement.API.Services
{
	public class UserService : IUserService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IWebHostEnvironment _env;

		public UserService(UserManager<ApplicationUser> userManager, IWebHostEnvironment env)
		{
			_userManager = userManager;
			_env = env;
		}

		public async Task<UserProfileDto> GetProfileAsync(string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);

			if (user == null) return null;

			return new UserProfileDto
			{
				FirstName = user.FirstName,
				LastName = user.LastName,
				PhoneNumber = user.PhoneNumber,
				NationalId = user.NationalId,
				Age = user.Age,
				SignatureUrl = user.SignaturePath
			};
		}

		public async Task<string> UpdateSignatureAsync(string userId, IFormFile file)
		{
			var user = await _userManager.FindByIdAsync(userId);

			if (user == null) return "User not found";

			if (file != null && file.Length > 0)
			{
				var uploadsFolder = Path.Combine(_env.WebRootPath, "signatures");
				Directory.CreateDirectory(uploadsFolder); // Ensure folder exists

				var fileName = $"{Guid.NewGuid()}_{file.FileName}";
				var filePath = Path.Combine(uploadsFolder, fileName);

				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await file.CopyToAsync(stream);
				}

				user.SignaturePath = $"/signatures/{fileName}";
				await _userManager.UpdateAsync(user);
				return "Signature uploaded successfully.";
			}

			return "No file provided.";
		}
	}
}
