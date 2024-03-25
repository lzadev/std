namespace Std.API.Models;

public class Employee : BaseModelWithAudit
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string LastName { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public string TelephoneNumber { get; set; }
    public string Address { get; set; }

    public override string ToString()
    {
        return $"{Name}-{LastName}-{DateOfBirth}-{TelephoneNumber}-{Address}";
    }
}
