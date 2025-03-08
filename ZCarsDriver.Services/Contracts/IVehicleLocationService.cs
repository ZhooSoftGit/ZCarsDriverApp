using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZCarsDriver.Services.Contracts
{
    public interface IVehicleLocationService
    {
        Task<ApiResponse<VehicleLocationDto>> GetVehicleLocationAsync(int vehicleId);
        Task<ApiResponse<bool>> UpdateVehicleLocationAsync(VehicleLocationDto location);
        Task<ApiResponse<bool>> UpsertVehicleLocation(VehicleLocationDto locationDto);
    }
}
