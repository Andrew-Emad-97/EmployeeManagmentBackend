﻿namespace EmployeeManagement.API.DTOs
{
	public class AuthResponseDto
	{
		public string Token { get; set; }
		public DateTime Expiration { get; set; }
		public string Role { get; set; }
		public string UserId { get; set; }
		public string Email { get; set; }
	}
}
