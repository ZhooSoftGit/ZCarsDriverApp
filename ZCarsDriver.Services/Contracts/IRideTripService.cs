using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhooCars.Model.DTOs;
using ZhooSoft.ServiceBase;

namespace ZCarsDriver.Services.Contracts
{
    public interface IRideTripService
    {
        Task<ApiResponse<bool>> CancelTripAsync(CancelTripDto request);
        Task<ApiResponse<RideTripDto>> EndTripAsync(EndTripDto request);
        Task<ApiResponse<TripPaymentDto>> GetTripPaymentDetailsAsync(int rideTripId);
        Task<ApiResponse<bool>> ReachPickupAsync(UpdateTripStatusDto request);
        Task<ApiResponse<bool>> StartTripAsync(UpdateTripStatusDto request);
        Task<ApiResponse<bool>> UpdateDistanceAsync(UpdateTripDistanceDto request);
    }
}
