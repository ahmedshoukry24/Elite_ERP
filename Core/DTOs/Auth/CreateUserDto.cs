using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Auth
{
    public class CreateUserDto
    {
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }


    }
}
