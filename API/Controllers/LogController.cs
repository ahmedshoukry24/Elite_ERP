using Core.DTOs;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LogController : ControllerBase
    {
        private readonly ILogService _logService;
        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpGet]
        public async Task<ActionResult<GlobalResponse>> GetLogs()
        {
            var response = await _logService.GetLogs();
            if (!response.IsSuccess)
            {
                return NotFound(response);
            }
            return Ok(response);

        }
    }
}
