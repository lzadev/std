using AutoMapper;
using Std.API.Emplyees.DTOs;
using Std.API.Emplyees.Interfaces;
using Std.API.Models;

namespace Std.API.Emplyees.Services;

public class BackUpEmployeeService(IBackUpEmployeeRepository repository, IMapper mapper) : IBackUpEmployeeService
{
    public void CreateOrUpdate(CreateOrUpdateEmployeeBackUpDTO entity)
    {
        var employee = repository.Get(x => x.Code.ToLower() == entity.Code.ToLower() && !x.Deleted && x.Active, true);

        if (employee != null)
        {
            mapper.Map(entity, employee);
            repository.SaveChanges();
            return;
        }

        var newEmployee = mapper.Map<Employee>(entity);
        repository.Create(newEmployee);
        repository.SaveChanges();
    }
}
