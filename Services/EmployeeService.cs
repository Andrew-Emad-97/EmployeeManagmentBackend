using EmployeeManagement.API.DTOs.Employee;
using EmployeeManagement.API.Models;
using Microsoft.AspNetCore.Identity;

namespace EmployeeManagement.API.Services
{
	public class EmployeeService : IEmployeeService
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public EmployeeService(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
		{
			var users = await _userManager.GetUsersInRoleAsync("Employee");
			return users.Select(u => new EmployeeDto
			{
				Id = u.Id,
				FirstName = u.FirstName,
				LastName = u.LastName,
				PhoneNumber = u.PhoneNumber,
				NationalId = u.NationalId,
				Age = u.Age,
				Signature = u.Signature,
				Email = u.Email
			});
		}

		public async Task<EmployeeDto> GetByIdAsync(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user == null) return null;

			return new EmployeeDto
			{
				Id = user.Id,
				FirstName = user.FirstName,
				LastName = user.LastName,
				PhoneNumber = user.PhoneNumber,
				NationalId = user.NationalId,
				Age = user.Age,
				Signature = user.Signature,
				Email = user.Email
			};
		}

		public async Task<string> CreateAsync(EmployeeCreateDto dto)
		{
			var user = new ApplicationUser
			{
				UserName = dto.Email,
				Email = dto.Email,
				FirstName = dto.FirstName,
				LastName = dto.LastName,
				PhoneNumber = dto.PhoneNumber,
				NationalId = dto.NationalId,
				Age = dto.Age,
				Signature = dto.Signature
			};

			var result = await _userManager.CreateAsync(user, dto.Password);
			if (!result.Succeeded)
				return string.Join(", ", result.Errors.Select(e => e.Description));

			await _userManager.AddToRoleAsync(user, "Employee");
			return "Employee created successfully.";
		}

		public async Task<bool> UpdateAsync(string id, EmployeeUpdateDto dto)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user == null) return false;

			user.FirstName = dto.FirstName;
			user.LastName = dto.LastName;
			user.PhoneNumber = dto.PhoneNumber;
			user.NationalId = dto.NationalId;
			user.Age = dto.Age;
			user.Signature = dto.Signature;

			var result = await _userManager.UpdateAsync(user);
			return result.Succeeded;
		}

		public async Task<bool> DeleteAsync(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user == null) return false;

			var result = await _userManager.DeleteAsync(user);
			return result.Succeeded;
		}
	}
}
