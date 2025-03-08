using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZCarsDriver.Services.Contracts
{
    public interface IVendorService
    {
        Task<ApiResponse<VendorDetailDto>> GetVendorByIdAsync(string? phoneNumber = null);
        Task<ApiResponse<PagedResponse<VendorListDto>>> GetVendorsByFilterAsync(VendorFilterDto filter);
        Task<ApiResponse<VendorDetailDto>> UpsertVendorAsync(string? phoneNumber, VendorDetailDto dto);
        Task<ApiResponse<bool>> Register(string? phoneNumber, VendorRegisterRequest vendorRegisterRequest);
    }

}
