using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZCarsDriver.Services.Contracts
{
    public interface IDriverShiftLogService
    {
        Task<ApiResponse<bool>> CheckInAsync(CheckInRequest request);
        Task<ApiResponse<bool>> CheckOutAsync(CheckOutRequest request);
        Task<ApiResponse<PagedResponse<DriverShiftLogDto>>> GetShiftLogsAsync(DateTime fromDate, DateTime toDate, int page, int pageSize);
    }
}
