using AutoMapper;
using Std.API.Emplyees.DTOs;
using Std.API.Models;

namespace Std.API.Emplyees.Profiles;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<Employee, EmployeeDTO>();
        CreateMap<CreateEmployeeDTO, Employee>();
        CreateMap<UpdateEmployeeDTO, Employee>();
        CreateMap<Employee, CreateOrUpdateEmployeeBackUpDTO>().ReverseMap();
    }
}