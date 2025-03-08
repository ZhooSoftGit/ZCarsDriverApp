using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhooCars.Common;
using ZhooCars.Model.DTOs;
using ZhooCars.Model.Response;
using ZhooSoft.ServiceBase;

namespace ZCarsDriver.Services.Contracts
{
    public interface IServiceRequestService
    {
        Task<ApiResponse<ServiceRequestDto>> CreateServiceRequestAsync(CreateServiceRequestDto requestDto);
        Task<ApiResponse<bool>> NotifyNearbyProvidersAsync(int ticketId);
        Task<ApiResponse<bool>> AssignServiceRequestAsync(int ticketId, int providerId);
        Task<ApiResponse<bool>> UpdateServiceStatusAsync(int ticketId, ServiceRequestStatus status);
        Task<ApiResponse<PagedResponse<ServiceRequestDto>>> GetFilteredServiceRequestsAsync(ServiceRequestFilterDto filter);
        Task<ApiResponse<ServiceRequestDetailsDto>> GetServiceRequestDetailsAsync(int ticketId);
    }
}
