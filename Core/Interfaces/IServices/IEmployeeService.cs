using Core.DTOs;
using Core.DTOs.HR.Employee;
using Core.Entities.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.IServices
{
    public interface IEmployeeService 
    {
        Task<GlobalResponse> AddEmployeeAsync(CreateEmployeeDto employee);
        Task<GlobalResponse> EditEmployee(UpdateEmployeeDto employeeDto);
        Task<GlobalResponse> DeleteEmployee(int id);
        Task<GlobalResponse> GetEmployees(FilterEmployees filterEmployees);
    }
}
