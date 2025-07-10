using Core.DTOs;
using Core.DTOs.Lookups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.IServices
{
    public interface ILogService
    {
        Task<bool> LogAction(CreateLogDto createLogDto);
        Task<GlobalResponse> GetLogs();
    }
}
