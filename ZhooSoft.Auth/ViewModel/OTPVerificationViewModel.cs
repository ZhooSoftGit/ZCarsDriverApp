using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
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
        private string _phoneNumber;

        [ObservableProperty]
        private bool _isTimerVisible = true;

        [ObservableProperty]
        private bool _isResendVisible;

        private int _secondsRemaining;

        public OTPVerificationViewModel()
        {
            _secondsRemaining = 60; // Set initial countdown time (1:30)
            StartTimer();
            SubmitCommand = new RelayCommand(OnSubmit);
            ResendCodeCommand = new RelayCommand(OnResendCode);
            ChangePhoneNumberCommand = new RelayCommand(OnChangePhoneNumber);
        }
        public ICommand SubmitCommand { get; }
        public ICommand ResendCodeCommand { get; }
        public ICommand ChangePhoneNumberCommand { get; }
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

        private void OnSubmit()
        {
            string enteredOTP = Otp1 + Otp2 + Otp3 + Otp4;
            // Validate OTP and proceed with verification
            
        }

        private void OnResendCode()
        {
            _secondsRemaining = 90;
            IsResendVisible = false;
            IsTimerVisible = true;
            StartTimer();
        }

        private void OnChangePhoneNumber()
        {
            _secondsRemaining = -1;
            _navigationService.PopAsync();
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
