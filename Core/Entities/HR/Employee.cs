using Core.Constants.Enums;
using Core.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.HR
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly HireDate { get; set; }
        //public string Email { get; set; }
        public EmployeeStatus Status { get; set; } = EmployeeStatus.Active;

        // Navigation props
        public Department Department { get; set; }
        public int DepartmentId { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
    }
}
