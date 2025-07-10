using API.Helpers;
using Core.DTOs;
using Core.DTOs.Auth;
using Core.Entities.Auth;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Services.Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        public AuthController(IAuthService service, UserManager<User> userManager, IConfiguration configuration)
        {
            _service = service;
            _userManager = userManager;
            _configuration = configuration;
        }


        [HttpPost("Login")]
        public async Task<ActionResult<GlobalResponse>> Signin(SigninDto signinDto)
        {
            var res = await _service.Signin(signinDto);
            if (!res.IsSuccess)
                return BadRequest(res);
            GlobalResponse<User> castRes = (GlobalResponse<User>)res;

            var token = await TokenHelper.CreateTokenResponse(castRes.Data, _userManager, _configuration);

            return Ok(new GlobalResponse<AuthResponseDto>
            {
                IsSuccess = castRes.IsSuccess,
                Message = castRes.Message,
                Data = new AuthResponseDto
                {
                    email = castRes.Data.Email,
                    id = castRes.Data.Id,
                    username = castRes.Data.UserName,
                    token = token
                }
            });
        }
    }
}
