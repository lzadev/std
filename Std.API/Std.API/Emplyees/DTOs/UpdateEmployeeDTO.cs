namespace Std.API.Emplyees.DTOs;

public class UpdateEmployeeDTO
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public string TelephoneNumber { get; set; }
    public string Address { get; set; }
}