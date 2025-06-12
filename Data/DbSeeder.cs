﻿using Microsoft.AspNetCore.Identity;

namespace EmployeeManagement.API.Data
{
	public class DbSeeder
	{
		public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
		{
			string[] roles = { "Admin", "Employee" };

			foreach (var role in roles)
			{
				if (!await roleManager.RoleExistsAsync(role))
				{
					await roleManager.CreateAsync(new IdentityRole(role));
				}
			}
		}
	}
}
