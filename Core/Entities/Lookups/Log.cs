using Core.Entities.Auth;
using Core.Entities.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Lookups
{
    public class Log
    {
        public int Id { get; set; }
        public string Description { get; set; }


        // Navigation props
        //public Employee Employee { get; set; }
        public int? EmployeeId { get; set; }

        //public User User { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
