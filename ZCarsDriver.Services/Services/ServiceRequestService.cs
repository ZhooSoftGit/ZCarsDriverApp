using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZCarsDriver.Services.Contracts;
using ZhooCars.Common;
using ZhooCars.Model.DTOs;
using ZhooCars.Model.Response;
using ZhooSoft.ServiceBase;

namespace ZCarsDriver.Services.Services
{
    public class ServiceRequestService : IServiceRequestService
    {
        private readonly IApiService _apiService;

        public ServiceRequestService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<ApiResponse<ServiceRequestDto>> CreateServiceRequestAsync(CreateServiceRequestDto requestDto)
        {
            return await _apiService.PostAsync<ServiceRequestDto>($"{ApiConstants.BaseUrl}{ApiConstants.CreateServiceRequest}", requestDto);
        }

        public async Task<ApiResponse<bool>> NotifyNearbyProvidersAsync(int ticketId)
        {
            return await _apiService.PostAsync<bool>(string.Format($"{ApiConstants.BaseUrl}{ApiConstants.NotifyNearbyProviders}", ticketId), null);
        }

        public async Task<ApiResponse<bool>> AssignServiceRequestAsync(int ticketId, int providerId)
        {
            return await _apiService.PostAsync<bool>(string.Format($"{ApiConstants.BaseUrl}{ApiConstants.AssignServiceRequest}", ticketId, providerId), null);
        }

        public async Task<ApiResponse<bool>> UpdateServiceStatusAsync(int ticketId, ServiceRequestStatus status)
        {
            return await _apiService.PostAsync<bool>(string.Format($"{ApiConstants.BaseUrl}{ApiConstants.UpdateServiceRequestStatus}", ticketId), new UpdateStatusDto { Status = status });
        }

        public async Task<ApiResponse<PagedResponse<ServiceRequestDto>>> GetFilteredServiceRequestsAsync(ServiceRequestFilterDto filter)
        {
            return await _apiService.PostAsync<PagedResponse<ServiceRequestDto>>($"{ApiConstants.BaseUrl}{ApiConstants.GetServiceRequests}", filter);
        }

        public async Task<ApiResponse<ServiceRequestDetailsDto>> GetServiceRequestDetailsAsync(int ticketId)
        {
            return await _apiService.GetAsync<ServiceRequestDetailsDto>(string.Format($"{ApiConstants.BaseUrl}{ApiConstants.GetServiceRequestDetails}", ticketId));
        }
    }
}
