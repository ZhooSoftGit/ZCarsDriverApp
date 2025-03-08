using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZCarsDriver.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IApiService _apiService;

        public UserService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<ApiResponse<UserDetailDto>> GetUserDashBoardDetailsAsync(string? phoneNumber = null)
        {
            var url = string.IsNullOrEmpty(phoneNumber) ? ApiConstants.GetUserDashBoardDetails : $"{ApiConstants.GetUserDashBoardDetails}?phoneNumber={phoneNumber}";
            return await _apiService.GetAsync<UserDetailDto>(url);
        }

        public async Task<ApiResponse<UserDetailDto>> GetUserDetailsAsync(string? phoneNumber = null)
        {
            var url = string.IsNullOrEmpty(phoneNumber) ? ApiConstants.GetUserDetails : $"{ApiConstants.GetUserDetails}?phoneNumber={phoneNumber}";
            return await _apiService.GetAsync<UserDetailDto>(url);
        }

        public async Task<ApiResponse<bool>> UpsertUserDetailsAsync(UserDetailDto userDetails, string? phoneNumber = null)
        {
            var url = string.IsNullOrEmpty(phoneNumber) ? ApiConstants.UpsertUserDetails : $"{ApiConstants.UpsertUserDetails}?phoneNumber={phoneNumber}";
            return await _apiService.PostAsync<bool>(url, userDetails);
        }
    }
}
