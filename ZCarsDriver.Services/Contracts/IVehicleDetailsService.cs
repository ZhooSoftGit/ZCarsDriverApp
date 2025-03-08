using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZCarsDriver.Services.Contracts
{
    public interface IVehicleDetailsService
    {
        Task<ApiResponse<VehicleDto>> GetVehicleByIdAsync(int id);
        Task<ApiResponse<PagedResponse<VehicleDto>>> GetVehiclesByFilterAsync(VehicleFilterDto filterDto);
        Task<ApiResponse<VehicleDto>> RegisterVehicleDetailsAsync(RegisterVehicleDto driverDto, string? phoneNumber = null);
        Task<ApiResponse<bool>> UpdateInsuranceAsync(int id, InsuranceUpdateDto insuranceDto);
        Task<ApiResponse<VehicleDto>> UpsertVehicleAsync(VehicleDto vehicleDto, string? phoneNumber = null);
    }
}
