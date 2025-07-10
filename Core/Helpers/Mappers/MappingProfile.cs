using AutoMapper;
using Core.DTOs.Auth;
using Core.DTOs.HR.Department;
using Core.DTOs.HR.Employee;
using Core.DTOs.Lookups;
using Core.Entities.Auth;
using Core.Entities.HR;
using Core.Entities.Lookups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserDto, User>();
            CreateMap<CreateEmployeeDto, Employee>().ForMember(x => x.Name, m => m.MapFrom(f => f.User.NameEn));
            CreateMap<Employee, EmployeeResponseDto>();
            CreateMap<UpdateEmployeeDto, Employee>();


            CreateMap<DepartmentDto, Department>();
            CreateMap<Department, DepartmentResponseDto>();

            CreateMap<CreateLogDto, Log>();
            CreateMap<Log, LogResponseDto>();
        }
    }
}
