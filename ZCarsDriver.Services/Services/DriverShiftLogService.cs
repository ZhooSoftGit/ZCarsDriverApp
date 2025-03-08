using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZCarsDriver.Services.Services
{
    public class DriverShiftLogService : IDriverShiftLogService
    {
        private readonly IApiService _apiService;

        public DriverShiftLogService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<ApiResponse<bool>> CheckInAsync(CheckInRequest request)
        {
            return await _apiService.PostAsync<bool>(ApiConstants.CheckIn, request);
        }

        public async Task<ApiResponse<bool>> CheckOutAsync(CheckOutRequest request)
        {
            return await _apiService.PostAsync<bool>(ApiConstants.CheckOut, request);
        }

        public async Task<ApiResponse<PagedResponse<DriverShiftLogDto>>> GetShiftLogsAsync(DateTime fromDate, DateTime toDate, int page, int pageSize)
        {
            var url = $"{ApiConstants.GetShiftLogs}?fromDate={fromDate:o}&toDate={toDate:o}&page={page}&pageSize={pageSize}";
            return await _apiService.GetAsync<PagedResponse<DriverShiftLogDto>>(url);
        }
    }
}
