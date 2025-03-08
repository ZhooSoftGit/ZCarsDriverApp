using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZCarsDriver.Services.Contracts;
using ZhooCars.Model.DTOs;
using ZhooSoft.ServiceBase;

namespace ZCarsDriver.Services.Services
{
    public class PeakHoursService : IPeakHoursService
    {
        private readonly IApiService _apiService;

        public PeakHoursService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<ApiResponse<List<PeakHourDto>>> GetAllPeakHoursAsync()
        {
            return await _apiService.GetAsync<List<PeakHourDto>>($"{ApiConstants.BaseUrl}{ApiConstants.PeakHours}");
        }

        public async Task<ApiResponse<PeakHourDto>> GetPeakHourByIdAsync(int id)
        {
            return await _apiService.GetAsync<PeakHourDto>($"{ApiConstants.BaseUrl}{string.Format(ApiConstants.PeakHourById, id)}");
        }

        public async Task<ApiResponse<PeakHourDto>> AddPeakHourAsync(PeakHourDto peakHour)
        {
            return await _apiService.PostAsync<PeakHourDto>($"{ApiConstants.BaseUrl}{ApiConstants.AddPeakHour}", peakHour);
        }
    }
}
