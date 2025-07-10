using Core.Constants;
using Core.Entities.HR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Auth
{
    public class User : IdentityUser<int>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }


        // Navigation properties
        public Employee Employee { get; set; }

        public string GetName(string lang = ContextData.english) => lang.ToLower() == ContextData.arabic ? NameAr : NameEn;

    }
}
