using EmployeeManagement.API.DTOs.Employee;
using EmployeeManagement.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeesController : ControllerBase
	{
		private readonly IEmployeeService _employeeService;

		public EmployeesController(IEmployeeService employeeService)
		{
			_employeeService = employeeService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var employees = await _employeeService.GetAllAsync();
			return Ok(employees);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(string id)
		{
			var emp = await _employeeService.GetByIdAsync(id);
			if (emp == null) return NotFound();
			return Ok(emp);
		}

		[HttpPost]
		public async Task<IActionResult> Create(EmployeeCreateDto dto)
		{
			var result = await _employeeService.CreateAsync(dto);
			return Ok(result);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(string id, EmployeeUpdateDto dto)
		{
			var updated = await _employeeService.UpdateAsync(id, dto);
			if (!updated) return NotFound();
			return Ok("Updated successfully.");
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(string id)
		{
			var deleted = await _employeeService.DeleteAsync(id);
			if (!deleted) return NotFound();
			return Ok("Deleted successfully.");
		}
	}
}
