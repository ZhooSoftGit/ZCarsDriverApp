using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhooCars.Model.DTOs;
using ZhooCars.Model.Response;
using ZhooSoft.ServiceBase;

namespace ZCarsDriver.Services.Contracts
{
    public interface IServiceProviderService
    {
        Task<ApiResponse<ServiceProviderDetailDto>> GetByIdAsync(int id);
        Task<ApiResponse<PagedResponse<ServiceProviderListDto>>> GetByFilterAsync(ServiceProviderFilterDto filter);
        Task<ApiResponse<bool>> RegisterAsync(int userId, RegisterServiceProviderDto requestDto);
        Task<ApiResponse<ServiceProviderDetailDto>> UpsertAsync(int userId, ServiceProviderDetailDto dto);
    }
}
