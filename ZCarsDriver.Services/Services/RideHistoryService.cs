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
    public class RideHistoryService : IRideHistoryService
    {
        private readonly IApiService _apiService;

        public RideHistoryService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<ApiResponse<IEnumerable<DriverRideHistoryDto>>> GetDriverRideHistoryAsync(int userId, RideHistoryFilterDto filterDto)
        {
            return await _apiService.PostAsync<IEnumerable<DriverRideHistoryDto>>(
                $"{ApiConstants.BaseUrl}{ApiConstants.DriverRideHistory}?userId={userId}", filterDto);
        }

        public async Task<ApiResponse<IEnumerable<PassengerRideHistoryDto>>> GetPassengerRideHistoryAsync(int userId, RideHistoryFilterDto filterDto)
        {
            return await _apiService.PostAsync<IEnumerable<PassengerRideHistoryDto>>(
                $"{ApiConstants.BaseUrl}{ApiConstants.PassengerRideHistory}?userId={userId}", filterDto);
        }
    }
}
