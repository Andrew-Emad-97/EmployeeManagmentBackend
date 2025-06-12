using EmployeeManagement.API.DTOs.Employee;

namespace EmployeeManagement.API.Services
{
	public interface IEmployeeService
	{
		Task<IEnumerable<EmployeeDto>> GetAllAsync();
		Task<EmployeeDto> GetByIdAsync(string id);
		Task<string> CreateAsync(EmployeeCreateDto dto);
		Task<bool> UpdateAsync(string id, EmployeeUpdateDto dto);
		Task<bool> DeleteAsync(string id);
	}
}
