using ZCarsDriver.Services;
using ZhooCars.Model.Response;
using ZhooSoft.ServiceBase;

namespace ZhooCars.Services
{
    public class AccountService : BaseService, IAccountService
    {
        #region Fields

        private readonly ApiService _apiService;

        #endregion

        #region Constructors

        public AccountService(ApiService apiService)
        {
            _apiService = apiService;
        }

        #endregion

        #region Methods

        public async Task<ApiResponse<TokenResponse?>> RefreshTokenAsync(string refreshToken)
        {
            return await _apiService.PostAsync<TokenResponse?>($"{ApiConstants.BaseUrl}{ApiConstants.AccountRefreshToken}", new { RefreshToken = refreshToken });
        }

        public async Task<ApiResponse<bool>> ReSendOtpAsync(string phoneNumber)
        {
            return await _apiService.PostAsync<bool>($"{ApiConstants.BaseUrl}{ApiConstants.AccountReSendOtp}", new { PhoneNumber = phoneNumber });
        }

        public async Task<ApiResponse<bool>> SendOtpAsync(string phoneNumber)
        {
            return await _apiService.PostAsync<bool>($"{ApiConstants.BaseUrl}{ApiConstants.AccountSendOtp}", new { PhoneNumber = phoneNumber });
        }

        public async Task<ApiResponse<TokenResponse?>> VerifyOtpAsync(string phoneNumber, string otpCode)
        {
            return await _apiService.PostAsync<TokenResponse?>($"{ApiConstants.BaseUrl}{ApiConstants.AccountVerifyOtp}", new { PhoneNumber = phoneNumber, Code = otpCode });
        }

        #endregion
    }
}
