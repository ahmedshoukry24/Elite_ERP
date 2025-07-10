using AutoMapper;
using Core.DTOs;
using Core.DTOs.HR.Employee;
using Core.Entities.HR;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<GlobalResponse>> AddEmployeeAsync([FromBody] CreateEmployeeDto employeeDto)
        {
            if (employeeDto == null || string.IsNullOrEmpty(employeeDto.User.Password))
            {
                return BadRequest(new GlobalResponse { IsSuccess = false, Message = "Invalid employee data or password." });
            }

            var response = await _employeeService.AddEmployeeAsync(employeeDto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Created(string.Empty, response);
        }

        [HttpPut]
        public async Task<ActionResult<GlobalResponse>> EditEmployee([FromBody] UpdateEmployeeDto employeeDto)
        {

            var response = await _employeeService.EditEmployee(employeeDto);
            if (!response.IsSuccess)
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                    return NotFound(response);

                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult<GlobalResponse>> DeleteEmployee([FromQuery] int id)
        {
            if (id <= 0)
                return new GlobalResponse { IsSuccess = false, Message = "Invalid employee ID.", StatusCode = HttpStatusCode.BadRequest };

            var response = await _employeeService.DeleteEmployee(id);
            if (!response.IsSuccess)
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                    return NotFound(response);
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<GlobalResponse>> GetEmployees([FromQuery] FilterEmployees filterEmployees)
        {

            var response = await _employeeService.GetEmployees(filterEmployees);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
