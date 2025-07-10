using Core.Constants.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.HR.Employee
{
    public class FilterEmployees
    {
        public string? Name { get; set; }
        public DateOnly? HireDate { get; set; }
        public EmployeeStatus? Status { get; set; }
        public int? DepartmentId { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string SortBy { get; set; }
        public bool IsDesc { get; set; }

    }
}
