using EmployeeManagement.API.DTOs;
using EmployeeManagement.API.DTOs.Attendace;

namespace EmployeeManagement.API.Services
{
	public interface IAttendanceService
	{
		Task<string> CheckInAsync(string userId);
		Task<List<AttendanceDto>> GetMyAttendanceAsync(string userId);
		Task<List<AttendanceDto>> GetAllAttendanceAsync(); 
		Task<Dictionary<string, double>> GetWeeklyHoursAsync();
	}
}
