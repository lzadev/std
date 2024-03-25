using AutoMapper;
using Std.API.Emplyees.DTOs;
using Std.API.Emplyees.Interfaces;
using Std.API.Exceptions;
using Std.API.Models;
using Std.API.SystemParameters.Interfaces;

namespace Std.API.Emplyees.Services;

public class EmplyeeService(IEmployeeRepository repository,ISystemParameterRepository systemParameterRepository, IMapper mapper) : IEmployeeService
{
    public async Task<IReadOnlyList<EmployeeDTO>> GetAll()
    {
        var result = await repository.GetAll(x => !x.Deleted && x.Active, false);
        return mapper.Map<IReadOnlyList<EmployeeDTO>>(result);
    }

    public async Task<EmployeeDTO> Get(int id)
    {
        var result = await repository.Get(x => x.Id == id && !x.Deleted && x.Active, false)
                                 ?? throw new NotFoundException("The employee does not exist");

        return mapper.Map<EmployeeDTO>(result);
    }

    public async Task<EmployeeDTO> Update(int id, UpdateEmployeeDTO entity)
    {
        var result = await repository.Get(x => x.Id == id && !x.Deleted && x.Active, true)
                         ?? throw new NotFoundException("The employee does not exist");

        mapper.Map(entity, result);
        await repository.SaveChangesAsync();
        return mapper.Map<EmployeeDTO>(result);
    }

    public async Task<EmployeeDTO> Create(CreateEmployeeDTO entity)
    {
        var newEntity = mapper.Map<Employee>(entity);
        var code = await systemParameterRepository.GetNextSequenceByParameterName("EMPLOYEE-CODE");
        newEntity.Code = code!;
        await repository.Create(newEntity);
        await repository.SaveChangesAsync();
        return mapper.Map<EmployeeDTO>(newEntity);
    }

    public async Task Delete(int id)
    {
        var result = await repository.Get(x => x.Id == id && !x.Deleted && x.Active, true)
                         ?? throw new NotFoundException("The employee does not exist");

        result.Active = false;
        result.Deleted = true;
        await repository.SaveChangesAsync();
    }
}