using AutoMapper;
using Core.DTOs;
using Core.DTOs.HR.Department;
using Core.Entities.HR;
using Core.Interfaces;
using Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GlobalResponse> CreateDepartment(DepartmentDto departmentDto)
        {
            var department = _mapper.Map<Department>(departmentDto);
            await _unitOfWork.DepartmentRepository.AddAsync(department);
            if (await _unitOfWork.SaveChangesAsync() <= 0)
            {
                return new GlobalResponse
                {
                    Message = "Failed to create department",
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
            return new GlobalResponse
            {
                Message = "Department created successfully",
                IsSuccess = true,
                StatusCode = HttpStatusCode.Created
            };
        }

        public async Task<GlobalResponse> GetDepartments()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            if (departments == null || !departments.Any())
            {
                return new GlobalResponse
                {
                    Message = "No departments found",
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.NotFound
                };
            }
            var departmentDtos = _mapper.Map<IEnumerable<DepartmentResponseDto>>(departments);
            return new GlobalResponse<IEnumerable<DepartmentResponseDto>>
            {
                Data = departmentDtos,
                Message = "Departments retrieved successfully",
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
