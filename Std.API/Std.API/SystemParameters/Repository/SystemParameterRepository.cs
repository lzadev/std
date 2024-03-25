using Microsoft.EntityFrameworkCore;
using Std.API.Context;
using Std.API.Models;
using Std.API.SystemParameters.Interfaces;

namespace Std.API.SystemParameters.Repository;

public class SystemParameterRepository(MainApplicationDbContext dbContext) : ISystemParameterRepository
{
    public readonly DbSet<SystemParameter> _table = dbContext.Set<SystemParameter>();

    public async Task<string?> GetNextSequenceByParameterName(string name)
    {
        var systemParameter = await _table.FirstOrDefaultAsync(x => x.Name == name);

        if (systemParameter == null) return null;

        var sequence = systemParameter.Sequence = (Convert.ToInt64(systemParameter.Sequence) + 1).ToString().PadLeft(6, '0');

        return $"{systemParameter.Prefix}-{sequence}";
    }
}
