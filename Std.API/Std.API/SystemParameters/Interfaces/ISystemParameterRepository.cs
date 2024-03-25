namespace Std.API.SystemParameters.Interfaces;

public interface ISystemParameterRepository
{
    Task<string?> GetNextSequenceByParameterName(string name);
}
