using FluentValidation;
using Std.API.Emplyees.DTOs;

namespace Std.API.Emplyees.Validators;

public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeDTO>
{
    public CreateEmployeeValidator()
    {

    }
}
