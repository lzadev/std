using Std.API.Models;
using System.Linq.Expressions;

namespace Std.API.Emplyees.Interfaces;

public interface IEmployeeRepository
{
    public Task<IReadOnlyList<Employee>> GetAll(Expression<Func<Employee, bool>> expression, bool trackEntity = true, params Expression<Func<Employee, object>>[]? includes);
    public Task<Employee?> Get(Expression<Func<Employee, bool>> expression, bool trackEntity = true, params Expression<Func<Employee, object>>[]? includes);
    public void Update(Employee entity);
    public Task Create(Employee entity);
    public Task SaveChangesAsync();
}
