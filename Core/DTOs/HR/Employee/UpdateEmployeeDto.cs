using Core.Constants.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.HR.Employee
{
    public class UpdateEmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly HireDate { get; set; }
        public EmployeeStatus Status { get; set; }
        public int DepartmentId { get; set; }

    }
}
