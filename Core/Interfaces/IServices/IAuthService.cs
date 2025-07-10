using Core.DTOs.Auth;
using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Auth;

namespace Core.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<GlobalResponse> CreateUser(User user, string password);
        Task<GlobalResponse> Signin(SigninDto signinDto);

    }
}
