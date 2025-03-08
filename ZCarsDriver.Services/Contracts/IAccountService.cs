using ZhooCars.Model.Response;
using ZhooSoft.ServiceBase;

namespace ZhooCars.Services
{
    #region Interfaces

    public interface IAccountService
    {
        Task<ApiResponse<TokenResponse?>> RefreshTokenAsync(string refreshToken);
        Task<ApiResponse<bool>> SendOtpAsync(string phoneNumber);
        Task<ApiResponse<bool>> ReSendOtpAsync(string phoneNumber);
        Task<ApiResponse<TokenResponse?>> VerifyOtpAsync(string phoneNumber, string otpCode);
    }

    #endregion

}
