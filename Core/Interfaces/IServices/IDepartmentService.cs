using Core.DTOs.HR.Department;
using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.IServices
{
    public interface IDepartmentService
    {
        Task<GlobalResponse> CreateDepartment(DepartmentDto departmentDto);
        Task<GlobalResponse> GetDepartments();
    }
}
