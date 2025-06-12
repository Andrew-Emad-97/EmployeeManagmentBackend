using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.API.Models
{
	public class Attendance
	{
		public int Id { get; set; }

		[Required]
		public string EmployeeId { get; set; }

		[ForeignKey(nameof(EmployeeId))]
		public ApplicationUser Employee { get; set; }

		public DateTime CheckInTime { get; set; }
	}
}
