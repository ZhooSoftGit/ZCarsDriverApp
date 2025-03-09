using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using ZhooCars.Services;
using ZhooSoft.Core;

namespace ZhooSoft.Auth.ViewModel
{
    public partial class OTPVerificationViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string _otp1;
        [ObservableProperty]
        private string _otp2;
        [ObservableProperty]
        private string _otp3;
        [ObservableProperty]
        private string _otp4;
        [ObservableProperty]
        private string _timerText;

        [ObservableProperty]
        private bool showError;

        [ObservableProperty]
        private string _phoneNumber;

        [ObservableProperty]
        private bool _isTimerVisible = true;

        [ObservableProperty]
        private bool _isResendVisible;

        private int _secondsRemaining;

        public OTPVerificationViewModel(IAccountService accountService)
        {
            _secondsRemaining = 60; // Set initial countdown time (1:30)
            StartTimer();
            SubmitCommand = new AsyncRelayCommand(OnSubmit);
            ResendCodeCommand = new RelayCommand(OnResendCode);
            ChangePhoneNumberCommand = new AsyncRelayCommand(OnChangePhoneNumber);
            _accountService = accountService;
        }
        public ICommand SubmitCommand { get; }
        public ICommand ResendCodeCommand { get; }
        public IAsyncRelayCommand ChangePhoneNumberCommand { get; }

        private readonly IAccountService _accountService;

        private async void StartTimer()
        {
            while (_secondsRemaining > 0)
            {
                TimerText = TimeSpan.FromSeconds(_secondsRemaining).ToString("mm\\:ss");
                await Task.Delay(1000);
                _secondsRemaining--;
            }
            TimerText = "";
            IsTimerVisible = false;
            IsResendVisible = true;
        }

        private async Task OnSubmit()
        {
            string enteredOtp = Otp1.Trim() + Otp2.Trim() + Otp3.Trim() + Otp4.Trim();


            if (enteredOtp.Length == 4)
            {
                var result = await _accountService.VerifyOtpAsync(PhoneNumber, enteredOtp);

                if (result.IsSuccess)
                {
                    ServiceHelper.GetService<IMainAppNavigation>().NavigateToMain(true);
                }
                else
                {
                    Otp1 = Otp2 = Otp3 = Otp4 = string.Empty;
                    ShowError = true;
                }
            }
            else
            {
                ShowError = true;
            }
        }

        private void OnResendCode()
        {
            Otp1 = string.Empty;
            Otp2 = string.Empty;
            Otp3 = string.Empty;
            Otp4 = string.Empty;
            _secondsRemaining = 90;
            IsResendVisible = false;
            IsTimerVisible = true;
            StartTimer();
        }

        private async Task OnChangePhoneNumber()
        {
            _secondsRemaining = -1;
            await _navigationService.PopAsync();
        }

        public override void OnAppearing()
        {
            base.OnAppearing();

            if (NavigationParams != null)
            {
                var str = NavigationParams["phoneNumber"].ToString();
                PhoneNumber = str;
            }
        }
    }
}
