using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.HR
{
    public class Department
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }

        // Navigation props
        public IEnumerable<Employee> Employees { get; set; }
    }
}
