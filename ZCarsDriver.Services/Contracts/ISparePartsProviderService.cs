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
    public interface ISparePartsProviderService
    {
        Task<ApiResponse<SparePartsProviderDetailDto>> UpsertAsync(int userId, SparePartsProviderDetailDto dto);
        Task<ApiResponse<SparePartsProviderDetailDto>> GetByIdAsync(int id);
        Task<ApiResponse<PagedResponse<SparPartsListDto>>> GetByFilterAsync(SparePartsProviderFilterDto filter);
        Task<ApiResponse<bool>> RegisterAsync(int userId, RegisterSparePartsProviderDto requestDto);
    }
}
