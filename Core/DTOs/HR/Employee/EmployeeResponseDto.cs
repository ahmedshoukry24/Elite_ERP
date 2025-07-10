using Core.Constants.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.HR.Employee
{
    public class EmployeeResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EmployeeStatus Status { get; set; }
        public int DepartmentId { get; set; }
        public int UserId { get; set; }
    }

    public class PaginationDataResponse
    {
        public int TotalCount { get; set; }
        public IEnumerable<EmployeeResponseDto> Data { get; set; }
    }
}
