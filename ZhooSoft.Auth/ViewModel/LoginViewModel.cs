using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Input;
using ZhooSoft.Auth.Views;
using ZhooSoft.Core;

namespace ZhooSoft.Auth.ViewModel
{
    public partial class LoginViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string _phoneNumber;


        [ObservableProperty]
        private string _errorMessage;

        [ObservableProperty]
        private bool _isPhoneValid;


        public ICommand SendOtpCommand { get; }

        public LoginViewModel()
        {
            SendOtpCommand = new Command(async () => await OnSendOtp());
        }

        private async Task OnSendOtp()
        {
            ErrorMessage = string.Empty;
            if (!ValidatePhoneNumber())
            {
                ErrorMessage = "Enter a valid phone number";
                return;
            }

            var nvparm = new Dictionary<string, object>()
            {
                {"phoneNumber", PhoneNumber },
                {"sessionId", "" }
            };
            //Send OTP from API
            await _navigationService.PushAsync(ServiceHelper.GetService<OTPVerificationPage>(), nvparm);
        }

        private bool ValidatePhoneNumber()
        {
            return Regex.IsMatch(PhoneNumber, @"^\d{10}$");
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.PropertyName == nameof(PhoneNumber))
            {
                if (ValidatePhoneNumber())
                {
                    IsPhoneValid = true;
                    ErrorMessage = string.Empty;
                }
                else
                {
                    IsPhoneValid = false;
                }
            }
        }
    }
}
