using Core.DTOs;
using Core.DTOs.Auth;
using Core.Entities.Auth;
using Core.Interfaces;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public AuthService(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;

        }
        public async Task<GlobalResponse> CreateUser(User user, string password)
        {

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                return new GlobalResponse { IsSuccess = false, Message = "faild to create user!" };

            var isRoleExist = await _roleManager.RoleExistsAsync("user");
            if (!isRoleExist)
                await _roleManager.CreateAsync(new IdentityRole<int> { Name = "user" });

            await _userManager.AddClaimsAsync(user, new List<Claim>{
                new Claim(ClaimTypes.Role, "user"),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                });

            await _userManager.AddToRoleAsync(user, "user");

            return new GlobalResponse<User> { Data = user, IsSuccess = true, Message = "User created successfully!" };

        }
        public async Task<GlobalResponse> Signin(SigninDto signinDto)
        {
            User user = await _userManager.FindByNameAsync(signinDto.Username);
            if (user == null)
                return new GlobalResponse { IsSuccess = false, Message = "invalid username or password" };

            var checkPassword = await _userManager.CheckPasswordAsync(user, signinDto.Password);
            if (!checkPassword)
                return new GlobalResponse { IsSuccess = false, Message = "invalid username or password" };
            return new GlobalResponse<User> { Data = user, IsSuccess = true, Message = "Logged in!" };
        }
    }
}
