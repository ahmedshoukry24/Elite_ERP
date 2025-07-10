using Core.DTOs;
using Core.Entities.Auth;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Seeding
{
    public class SeedUser
    {
        private readonly ILogger<SeedUser> _logger;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public SeedUser(ILogger<SeedUser> logger, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        public async Task SeedingUserAsync()
        {
            await _unitOfWork.BeginTransactionAsync();
            User user = await _userManager.FindByNameAsync("superadmin");


            if (user is not null)
            {
                _logger.LogInformation("There is a superadmin user!");
                await _unitOfWork.RollbackTransactionAsync();
                return;
            }

            var res = await CreateUser(user);
            if (!res.IsSuccess)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return;
            }
            else
            {
                await _unitOfWork.SaveChangesAsync();
                user = res.Data;
            }

            if (!await AddClaims(user))
            {
                await _unitOfWork.RollbackTransactionAsync();
                return;
            }




            var isRoleExist = await _roleManager.RoleExistsAsync("Superadmin");
            if (!isRoleExist)
            {
                await _roleManager.CreateAsync(new IdentityRole<int> { Name = "Superadmin" });
                _logger.LogInformation("superadmin Role Created!");
            }


            var inRoleRes = await _userManager.IsInRoleAsync(user, "Superadmin");
            if (!inRoleRes)
            {
                await _userManager.AddToRoleAsync(user, "Superadmin");
                _logger.LogInformation("assigned the superadmin role to the superadmin!");
            }

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitChangesAsync();

        }

        private async Task<GlobalResponse<User>> CreateUser(User user)
        {
            user = new User
            {
                NameEn = "SuperAdmin",
                NameAr = "SuperAdmin",
                Email = "superadmin@email.com",
                UserName = "superadmin",
                EmailConfirmed = true
            };
            var res = await _userManager.CreateAsync(user, "superadmin@123");
            string errorMsg = "";
            if (!res.Succeeded)
            {
                foreach (var err in res.Errors)
                    errorMsg = string.Join(";", err);

                _logger.LogInformation($"Couldn't seed superadmin! {errorMsg}");
                return new GlobalResponse<User> { IsSuccess = false };
            }
            _logger.LogInformation("super admin seeded!");
            return new GlobalResponse<User> { IsSuccess = true, Data = user };
        }

        private async Task<bool> AddClaims(User user)
        {
            var claims = new List<Claim>{
                new Claim(ClaimTypes.Role, "Superadmin"),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };
            var res = await _userManager.AddClaimsAsync(user, claims);
            if (!res.Succeeded)
            {
                _logger.LogInformation("claims couldn't be added to superadmin!");
                return false;
            }
            _logger.LogInformation("claims added to superadmin!");
            return true;
        }

    }
}
