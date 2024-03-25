namespace Std.API.Models;

public class SystemParameter
{
    public int Id { get; set; }
    public required string Sequence { get; set; }
    public required string Prefix { get; set; }
    public required string Name { get; set; }
}
