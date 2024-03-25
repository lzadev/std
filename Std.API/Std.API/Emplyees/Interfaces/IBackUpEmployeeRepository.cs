using Std.API.Models;
using System.Linq.Expressions;

namespace Std.API.Emplyees.Interfaces;

public interface IBackUpEmployeeRepository
{
    Employee? Get(Expression<Func<Employee, bool>> expression, bool trackEntity = true, params Expression<Func<Employee, object>>[]? includes);
    void Create(Employee employee);
    void Update(Employee employee);
    void SaveChanges();
}
