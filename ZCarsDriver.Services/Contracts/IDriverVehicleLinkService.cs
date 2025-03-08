using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhooCars.Model.DTOs;
using ZhooSoft.ServiceBase;

namespace ZCarsDriver.Services.Contracts
{
    public interface IDriverVehicleLinkService
    {
        Task<ApiResponse<bool>> DeleteVehicleLinkAsync(int linkId);
        Task<ApiResponse<List<DriverVehicleLinkDto>>> GetLinkedVehicleAsync(int userId);
        Task<ApiResponse<List<VehicleDto>>> GetVehiclesByVendorPhoneNumberAsync(string vendorPhoneNumber);
        Task<ApiResponse<bool>> RequestVehicleLinkAsync(RequestVehicleLinkDto request);
        Task<ApiResponse<bool>> VerifyVehicleLinkAsync(VerifyVehicleLinkDto request);
    }
}
