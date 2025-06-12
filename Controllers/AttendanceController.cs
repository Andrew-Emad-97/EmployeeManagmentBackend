using EmployeeManagement.API.Models;
using EmployeeManagement.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeeManagement.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AttendanceController : ControllerBase
	{
		private readonly IAttendanceService _attendanceService;
		private readonly UserManager<ApplicationUser> _userManager;

		public AttendanceController(IAttendanceService attendanceService, UserManager<ApplicationUser> userManager)
		{
			_attendanceService = attendanceService;
			_userManager = userManager;
		}

		[HttpPost("check-in")]
		[Authorize(Roles = "Employee")]
		public async Task<IActionResult> CheckIn()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var messg = await _attendanceService.CheckInAsync(userId);
			//return Ok(result);
			//return result ? Ok("Checked in successfully.") : BadRequest("You already checked in.");
			if (messg == "Checked in successfully.")
				return Ok(messg);

			return BadRequest(messg);

		}

		[HttpGet("my-attendance")]
		[Authorize(Roles = "Employee")]
		public async Task<IActionResult> GetMyAttendance()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var result = await _attendanceService.GetMyAttendanceAsync(userId);
			return Ok(result);
		}

		[HttpGet("all")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetAllAttendance()
		{
			var result = await _attendanceService.GetAllAttendanceAsync();
			return Ok(result);
		}

		[HttpGet("weekly-summary")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetWeeklySummary()
		{
			var result = await _attendanceService.GetWeeklyHoursAsync();
			return Ok(result);
		}
	}
}
