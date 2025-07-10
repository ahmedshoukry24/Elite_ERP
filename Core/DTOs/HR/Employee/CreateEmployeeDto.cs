using Core.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.HR.Employee
{
    public class CreateEmployeeDto
    {
        public DateOnly HireDate { get; set; }
        public int DepartmentId { get; set; }
        public CreateUserDto User { get; set; }
    }
}
