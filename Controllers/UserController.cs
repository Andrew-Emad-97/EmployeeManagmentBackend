using EmployeeManagement.API.DTOs.User;
using EmployeeManagement.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeeManagement.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet("profile")]
		[Authorize]
		public async Task<IActionResult> GetProfile()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var result = await _userService.GetProfileAsync(userId);
			return Ok(result);
		}

		[HttpPost("signature")]
		[Authorize]
		public async Task<IActionResult> UploadSignature([FromForm] UpdateSignatureDto dto)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var result = await _userService.UpdateSignatureAsync(userId, dto.Signature);
			return Ok(result);
		}
	}
}
