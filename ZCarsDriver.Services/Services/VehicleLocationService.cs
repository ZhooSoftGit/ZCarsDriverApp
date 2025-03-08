using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZCarsDriver.Services.Services
{
    public class VehicleLocationService : IVehicleLocationService
    {
        private readonly IApiService _apiService;

        public VehicleLocationService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<ApiResponse<VehicleLocationDto>> GetVehicleLocationAsync(int vehicleId)
        {
            var url = $"{ApiConstants.GetVehicleLocation}/{vehicleId}";
            return await _apiService.GetAsync<VehicleLocationDto>(url);
        }

        public async Task<ApiResponse<bool>> UpdateVehicleLocationAsync(VehicleLocationDto location)
        {
            return await _apiService.PostAsync<bool>(ApiConstants.UpdateVehicleLocation, location);
        }

        public async Task<ApiResponse<bool>> UpsertVehicleLocation(VehicleLocationDto locationDto)
        {
            return await _apiService.PostAsync<bool>(ApiConstants.UpdateVehicleStatus, locationDto);
        }
    }
}
