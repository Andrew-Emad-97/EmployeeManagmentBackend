namespace EmployeeManagement.API.DTOs.Attendace
{
	public class EmployeeAttendanceDto
	{
		public string EmployeeId { get; set; }
		public string FullName { get; set; }
		public double TotalHoursThisWeek { get; set; }
		public List<CheckInRecordDto> Records { get; set; }
	}

	public class CheckInRecordDto
	{
		public DateTime CheckInDate { get; set; }
		public TimeSpan CheckInTime { get; set; }
	}
}
