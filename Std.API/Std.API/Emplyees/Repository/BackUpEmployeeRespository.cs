using Microsoft.EntityFrameworkCore;
using Std.API.Context;
using Std.API.Emplyees.Interfaces;
using Std.API.Models;
using System.Linq.Expressions;

namespace Std.API.Emplyees.Repository;

public class BackUpEmployeeRespository(BackUpApplicationContext context) : IBackUpEmployeeRepository
{
    private readonly DbSet<Employee> _table = context.Set<Employee>();

    public Employee? Get(Expression<Func<Employee, bool>> expression, bool trackEntity = true, params Expression<Func<Employee, object>>[]? includes)
    {
        var query = _table.Where(expression);

        var result = includes != null ? includes.Aggregate(query, (current, property) => current.Include(property)) : query;

        return trackEntity ? result.FirstOrDefault() : result.AsNoTracking().FirstOrDefault();
    }
    public void Create(Employee employee)
    {
        _table.Add(employee);
    }

    public void Update(Employee employee)
    {
        _table.Update(employee);
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }
}