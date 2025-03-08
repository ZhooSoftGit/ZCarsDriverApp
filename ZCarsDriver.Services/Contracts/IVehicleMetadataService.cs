using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhooCars.Model.DTOs;
using ZhooSoft.ServiceBase;

namespace ZCarsDriver.Services.Contracts
{
    public interface IVehicleMetadataService
    {
        Task<ApiResponse<IEnumerable<VehicleModelDto>>> GetVehicleModelsAsync();
        Task<ApiResponse<IEnumerable<VehicleModelDto>>> GetVehicleModelsByTypeAsync(int typeId);
        Task<ApiResponse<IEnumerable<VehicleTypeDto>>> GetVehicleTypesAsync();
    }
}
