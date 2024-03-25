using Microsoft.AspNetCore.Mvc;
using Std.API.Emplyees.DTOs;
using Std.API.Emplyees.Interfaces;

namespace Std.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController(IEmployeeService employeeService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<EmployeeDTO>>> GetAll()
    {
        return Ok(await employeeService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeDTO>> Get(int id)
    {
        return Ok(await employeeService.Get(id));
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeDTO>> Create([FromBody] CreateEmployeeDTO dto)
    {
        return Ok(await employeeService.Create(dto));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<EmployeeDTO>> Update(int id, UpdateEmployeeDTO dto)
    {
        return Ok(await employeeService.Update(id, dto));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await employeeService.Delete(id);
        return NoContent();
    }
}