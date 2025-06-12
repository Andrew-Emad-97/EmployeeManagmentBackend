using EmployeeManagement.API.Data;
using EmployeeManagement.API.DTOs.Attendace;
using EmployeeManagement.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.API.Services
{
	public class AttendanceService : IAttendanceService
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;

		public AttendanceService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public async Task<string> CheckInAsync(string userId)
		{
			var now = DateTime.Now;
			var today = now.Date;

			if (now.TimeOfDay < new TimeSpan(7, 30, 0) || now.TimeOfDay > new TimeSpan(9, 0, 0))
				return "Check-in is allowed only between 7:30 AM and 9:00 AM.";

			var alreadyCheckedIn = await _context.Attendances
				.AnyAsync(a => a.EmployeeId == userId && a.CheckInTime.Date == today);

			if (alreadyCheckedIn)
				return "You have already checked in today.";

			var attendance = new Attendance
			{
				EmployeeId = userId,
				CheckInTime = now
			};

			_context.Attendances.Add(attendance);
			await _context.SaveChangesAsync();
			return "Check-in successful.";
		}

		public async Task<List<AttendanceDto>> GetMyAttendanceAsync(string userId)
		{
			return await _context.Attendances
				.Where(a => a.EmployeeId == userId)
				.OrderByDescending(a => a.CheckInTime)
				.Select(a => new AttendanceDto
				{
					Id = a.Id,
					EmployeeName = a.Employee.FirstName + " " + a.Employee.LastName,
					CheckInTime = a.CheckInTime
				})
				.ToListAsync();
		}

		public async Task<List<AttendanceDto>> GetAllAttendanceAsync()
		{
			return await _context.Attendances
				.Include(a => a.Employee)
				.OrderByDescending(a => a.CheckInTime)
				.Select(a => new AttendanceDto
				{
					Id = a.Id,
					EmployeeName = a.Employee.FirstName + " " + a.Employee.LastName,
					CheckInTime = a.CheckInTime
				})
				.ToListAsync();
		}

		public async Task<Dictionary<string, double>> GetWeeklyHoursAsync()
		{
			var startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
			var endOfWeek = startOfWeek.AddDays(7);

			var weekly = await _context.Attendances
				.Include(a => a.Employee)
				.Where(a => a.CheckInTime >= startOfWeek && a.CheckInTime < endOfWeek)
				.GroupBy(a => a.EmployeeId)
				.Select(g => new
				{
					EmployeeName = g.First().Employee.FirstName + " " + g.First().Employee.LastName,
					Days = g.Count()
				})
				.ToListAsync();

			return weekly.ToDictionary(e => e.EmployeeName, e => e.Days * 8.0); 
		}
	}
}
