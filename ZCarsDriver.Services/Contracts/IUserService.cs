using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZCarsDriver.Services.Contracts
{
    public interface IUserService
    {
        Task<ApiResponse<UserDetailDto>> GetUserDashBoardDetailsAsync(string? phoneNumber = null);
        Task<ApiResponse<UserDetailDto>> GetUserDetailsAsync(string? phoneNumber = null);
        Task<ApiResponse<bool>> UpsertUserDetailsAsync(UserDetailDto userDetails, string? phoneNumber = null);
    }
}
