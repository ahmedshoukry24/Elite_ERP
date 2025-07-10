using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.HR.Department
{
    public class DepartmentDto
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
    }
    public class DepartmentResponseDto : DepartmentDto
    {
        public int Id { get; set; }
    }
}
