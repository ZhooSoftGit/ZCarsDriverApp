using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhooCars.Model.DTOs;
using ZhooSoft.ServiceBase;

namespace ZCarsDriver.Services.Contracts
{
    public interface ITaxiBookingService
    {
        Task<ApiResponse<List<AvailableCabModel>>> SearchAvailableCabsAsync(double latitude, double longitude);
        Task<ApiResponse<FareDetailsModel>> CalculateFareAsync(FareCalculationRequest request);
        Task<ApiResponse<List<FareRequestModel>>> GetFareOptionsAsync(FareRequestModel request);
        Task<ApiResponse<RideRequestDto>> BookRideAsync(RideRequestModel request);
        Task<ApiResponse<RideTripDto>> AcceptRideAsync(AcceptRideRequest request);
        Task<ApiResponse<bool>> CancelRideRequestAsync(int rideRequestId);
        Task<ApiResponse<bool>> SkipBidAsync(UpdateBidRequestModel request);
    }

}
