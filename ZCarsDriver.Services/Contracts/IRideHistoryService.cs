using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhooCars.Model.DTOs;
using ZhooSoft.ServiceBase;

namespace ZCarsDriver.Services.Contracts
{
    public interface IRideHistoryService
    {
        Task<ApiResponse<IEnumerable<DriverRideHistoryDto>>> GetDriverRideHistoryAsync(int userId, RideHistoryFilterDto filterDto);
        Task<ApiResponse<IEnumerable<PassengerRideHistoryDto>>> GetPassengerRideHistoryAsync(int userId, RideHistoryFilterDto filterDto);
    }
}
