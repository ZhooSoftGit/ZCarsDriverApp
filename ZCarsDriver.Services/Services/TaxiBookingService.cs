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
    public class TaxiBookingService : ITaxiBookingService
    {
        private readonly IApiService _apiService;

        public TaxiBookingService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<ApiResponse<List<AvailableCabModel>>> SearchAvailableCabsAsync(double latitude, double longitude)
        {
            var url = $"{ApiConstants.SearchCabs}?latitude={latitude}&longitude={longitude}";
            return await _apiService.GetAsync<List<AvailableCabModel>>(url);
        }

        public async Task<ApiResponse<FareDetailsModel>> CalculateFareAsync(FareCalculationRequest request)
        {
            return await _apiService.PostAsync<FareDetailsModel>(ApiConstants.CalculateFare, request);
        }

        public async Task<ApiResponse<List<FareRequestModel>>> GetFareOptionsAsync(FareRequestModel request)
        {
            return await _apiService.PostAsync<List<FareRequestModel>>(ApiConstants.GetFareOptions, request);
        }

        public async Task<ApiResponse<RideRequestDto>> BookRideAsync(RideRequestModel request)
        {
            return await _apiService.PostAsync<RideRequestDto>(ApiConstants.BookRide, request);
        }

        public async Task<ApiResponse<RideTripDto>> AcceptRideAsync(AcceptRideRequest request)
        {
            return await _apiService.PostAsync<RideTripDto>(ApiConstants.AcceptRide, request);
        }

        public async Task<ApiResponse<bool>> CancelRideRequestAsync(int rideRequestId)
        {
            var url = string.Format(ApiConstants.CancelRideRequest, rideRequestId);
            return await _apiService.PostAsync<bool>(url, null);
        }

        public async Task<ApiResponse<bool>> SkipBidAsync(UpdateBidRequestModel request)
        {
            return await _apiService.PostAsync<bool>(ApiConstants.SkipBid, request);
        }
    }

}
