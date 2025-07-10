using AutoMapper;
using Core.DTOs;
using Core.DTOs.HR.Employee;
using Core.DTOs.Lookups;
using Core.Entities.HR;
using Core.Interfaces;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogService _logService;
        public EmployeeService(IUnitOfWork unitOfWork, IAuthService authService, IMapper mapper,
            IHttpContextAccessor httpContextAccessor, ILogService logService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logService = logService;
        }

        public async Task<GlobalResponse> AddEmployeeAsync(CreateEmployeeDto employeeDto)
        {

            Employee employee = _mapper.Map<Employee>(employeeDto);
            await _unitOfWork.BeginTransactionAsync();


            var userRes = await _authService.CreateUser(employee.User, employeeDto.User.Password);
            if (!userRes.IsSuccess)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return userRes;
            }

            employee = await _unitOfWork.EmployeeRepository.AddAsync(employee);

            if (await _unitOfWork.SaveChangesAsync() <= 0)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return new GlobalResponse
                {
                    IsSuccess = false,
                    Message = "Failed to add employee!"
                };
            }

            // handle Logs
            await HandleLogDescription(employee.Id, 1);

            await _unitOfWork.CommitChangesAsync();
            EmployeeResponseDto employeeResponseDto = _mapper.Map<EmployeeResponseDto>(employee);
            return new GlobalResponse<EmployeeResponseDto>
            {
                IsSuccess = true,
                Message = "Employee added successfully!",
                Data = employeeResponseDto,
                StatusCode = HttpStatusCode.Created
            };
        }

        public async Task<GlobalResponse> EditEmployee(UpdateEmployeeDto employeeDto)
        {
            if (employeeDto == null || employeeDto.Id <= 0)
                return new GlobalResponse { IsSuccess = false, Message = "Invalid employee data.", StatusCode = HttpStatusCode.BadRequest };

            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(employeeDto.Id);
            if (employee == null)
                return new GlobalResponse { IsSuccess = false, Message = "Employee not found.", StatusCode = HttpStatusCode.NotFound };

            _mapper.Map(employeeDto, employee);
            _unitOfWork.EmployeeRepository.Update(employee);
            if (await _unitOfWork.SaveChangesAsync() <= 0)
                return new GlobalResponse { IsSuccess = false, Message = "Failed to update employee.", StatusCode = HttpStatusCode.BadRequest };

            // handle Logs
            await HandleLogDescription(employee.Id, 2);

            return new GlobalResponse<EmployeeResponseDto>
            {
                IsSuccess = true,
                Message = "Employee updated successfully!",
                Data = _mapper.Map<EmployeeResponseDto>(employee),
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<GlobalResponse> DeleteEmployee(int id)
        {
            var emp = await _unitOfWork.EmployeeRepository.GetByIdAsync(id);
            if (emp == null)
                return new GlobalResponse { IsSuccess = false, Message = "Employee not found.", StatusCode = HttpStatusCode.NotFound };

            // handle Logs
            await HandleLogDescription(id, 3);

            _unitOfWork.EmployeeRepository.Delete(emp);
            if (await _unitOfWork.SaveChangesAsync() <= 0)
                return new GlobalResponse { IsSuccess = false, Message = "Faild to delete employee!" };

            

            return new GlobalResponse { IsSuccess = true, Message = "Deleted!" };
        }

        public async Task<GlobalResponse> GetEmployees(FilterEmployees filterEmployees)
        {


            var query = _unitOfWork.EmployeeRepository.GetAllQueryableAsNoTracking().Where(x =>
                // Name filtering
                (string.IsNullOrEmpty(filterEmployees.Name) ||
                    x.Name.ToLower().Contains(filterEmployees.Name.ToLower()))

                // DepartmentId filtering
                && (!filterEmployees.DepartmentId.HasValue || filterEmployees.DepartmentId <= 0 || x.DepartmentId == filterEmployees.DepartmentId)

                // Status filtering
                && (!filterEmployees.Status.HasValue || x.Status == filterEmployees.Status)

                // HireDate filtering
                && (!filterEmployees.HireDate.HasValue || x.HireDate == filterEmployees.HireDate.Value)
            );


            query = SortData(query, filterEmployees.SortBy, filterEmployees.IsDesc);

            var totalCount = await query.CountAsync();
            var employees = await query
                .Skip((filterEmployees.PageNumber - 1) * filterEmployees.PageSize)
                .Take(filterEmployees.PageSize)
                .ToListAsync();



            if (employees == null || !employees.Any())
                return new GlobalResponse { IsSuccess = false, Message = "No employees found.", StatusCode = HttpStatusCode.NotFound };
            var employeeDtos = _mapper.Map<IEnumerable<EmployeeResponseDto>>(employees);



            return new GlobalResponse<PaginationDataResponse>
            {
                IsSuccess = true,
                Message = "Employees retrieved successfully.",
                Data = new PaginationDataResponse { Data = employeeDtos, TotalCount = totalCount },
                StatusCode = HttpStatusCode.OK
            };

        }


        private IQueryable<Employee> SortData(IQueryable<Employee> query, string SortBy, bool IsDesc)
        {
            switch (SortBy?.ToLower())
            {
                case "name":
                    query = IsDesc
                        ? query.OrderByDescending(x => x.Name)
                        : query.OrderBy(x => x.Name);
                    break;

                case "hiredate":
                    query = IsDesc
                        ? query.OrderByDescending(x => x.HireDate)
                        : query.OrderBy(x => x.HireDate);
                    break;

                default:
                    query = query.OrderBy(x => x.Id);
                    break;
            }
            return query;
        }

        private int GetCurrentUserId()
        {
            int.TryParse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userID);

            return userID;
        }

        private async Task HandleLogDescription(int empId, int action)
        {
            var userId = GetCurrentUserId();
            if (userId <= 0)
                return;

            string description = action switch // Action could be enum 
            {
                1 => $"Employee with ID {empId} was created by user with Id {userId}.",
                2 => $"Employee with ID {empId} was updated  by user with Id {userId}.",
                3 => $"Employee with ID {empId} was deleted  by user with Id {userId}.",
                _ => "Unknown action"
            };


            bool res = await _logService.LogAction(new CreateLogDto
            {
                Description = description,
                UserId = userId,
                EmployeeId = empId
            });
            if (res)
                await _unitOfWork.SaveChangesAsync();
        }

    }
}
