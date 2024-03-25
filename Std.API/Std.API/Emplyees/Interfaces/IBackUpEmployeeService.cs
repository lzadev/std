using Std.API.Emplyees.DTOs;

namespace Std.API.Emplyees.Interfaces;

public interface IBackUpEmployeeService
{
    void CreateOrUpdate(CreateOrUpdateEmployeeBackUpDTO entity);
}