﻿using EmployeeManagement.API.DTOs;
using EmployeeManagement.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register(RegisterDto dto)
		{
			var result = await _authService.RegisterAsync(dto);
			return Ok(result);
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginDto dto)
		{
			var result = await _authService.LoginAsync(dto);
			if (result == null)
				return Unauthorized("Invalid email or password");

			return Ok(result);
		}
	}
}
