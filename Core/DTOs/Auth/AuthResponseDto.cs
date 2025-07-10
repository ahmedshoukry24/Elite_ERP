using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Auth
{
    public class AuthResponseDto
    {
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string token { get; set; }
    }
}
