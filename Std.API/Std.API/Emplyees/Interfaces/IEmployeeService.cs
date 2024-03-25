using Std.API.Emplyees.DTOs;

namespace Std.API.Emplyees.Interfaces;

public interface IEmployeeService
{
    Task<IReadOnlyList<EmployeeDTO>> GetAll();
    Task<EmployeeDTO> Get(int id);
    Task<EmployeeDTO> Create(CreateEmployeeDTO entity);
    Task<EmployeeDTO> Update(int id, UpdateEmployeeDTO entity);
    Task Delete(int id);
}