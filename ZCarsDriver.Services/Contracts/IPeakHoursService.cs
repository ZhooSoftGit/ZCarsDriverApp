using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhooCars.Model.DTOs;
using ZhooSoft.ServiceBase;

namespace ZCarsDriver.Services.Contracts
{
    public interface IPeakHoursService
    {
        Task<ApiResponse<List<PeakHourDto>>> GetAllPeakHoursAsync();
        Task<ApiResponse<PeakHourDto>> GetPeakHourByIdAsync(int id);
        Task<ApiResponse<PeakHourDto>> AddPeakHourAsync(PeakHourDto peakHour);
    }
}
