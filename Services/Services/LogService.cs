using AutoMapper;
using Core.DTOs;
using Core.DTOs.Lookups;
using Core.Entities.Lookups;
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
    public class LogService : ILogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LogService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> LogAction(CreateLogDto createLogDto)
        {

            var log = _mapper.Map<Log>(createLogDto);

            await _unitOfWork.LogRepository.AddAsync(log);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<GlobalResponse> GetLogs()
        {
            var logs = await _unitOfWork.LogRepository.GetAllAsync();
            if (logs == null || !logs.Any())
            {
                return new GlobalResponse
                {
                    Message = "No logs found",
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.NotFound
                };
            }
            var logDtos = _mapper.Map<IEnumerable<LogResponseDto>>(logs);

            return new GlobalResponse<IEnumerable<LogResponseDto>>
            {
                Data = logDtos,
                Message = "Logs retrieved successfully",
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK
            };

        }
    }
}
