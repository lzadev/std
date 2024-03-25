using Microsoft.EntityFrameworkCore;
using Std.API.Context;
using Std.API.Emplyees.Interfaces;
using Std.API.Models;
using System.Linq.Expressions;

namespace Std.API.Emplyees.Repository;

public class MainEmployeeRepository(MainApplicationDbContext context) : IEmployeeRepository
{
    private readonly DbSet<Employee> _table = context.Set<Employee>();

    public async Task<Employee?> Get(Expression<Func<Employee, bool>> expression, bool trackEntity = true, params Expression<Func<Employee, object>>[]? includes)
    {
        var query = _table.Where(expression);

        var result = includes != null ? includes.Aggregate(query, (current, property) => current.Include(property)) : query;

        return trackEntity ? await result.FirstOrDefaultAsync() : await result.AsNoTracking().FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<Employee>> GetAll(Expression<Func<Employee, bool>> expression, bool trackEntity = true, params Expression<Func<Employee, object>>[]? includes)
    {
        var query = _table.Where(expression);

        var result = includes != null ? includes.Aggregate(query, (current, property) => current.Include(property)) : query;

        return trackEntity ? await result.ToListAsync() : await result.AsNoTracking().ToListAsync();
    }

    public async Task Create(Employee entity)
    {
        await _table.AddAsync(entity);
    }

    public void Update(Employee entity)
    {
        _table.Update(entity);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}