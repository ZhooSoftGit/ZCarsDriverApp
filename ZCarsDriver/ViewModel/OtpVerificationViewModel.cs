using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using ZhooSoft.Core;

namespace ZCarsDriver.ViewModel
{
    public partial class OtpVerificationViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string _otp1, _otp2, _otp3, _otp4;

        [ObservableProperty]
        private Color _otpBorderColor = Colors.LightGray;

        [ObservableProperty]
        private string resendOtpText = "Resend OTP (60s)";

        [ObservableProperty]
        private bool isResendEnabled = false;

        private int countdown = 60;
        private System.Timers.Timer timer;

        public ICommand VerifyOtpCommand { get; }
        public ICommand ResendOtpCommand { get; }

        public OtpVerificationViewModel()
        {
            VerifyOtpCommand = new RelayCommand(VerifyOtp);
            ResendOtpCommand = new RelayCommand(ResendOtp);
            StartCountdown();
        }

        private void VerifyOtp()
        {
            if (string.IsNullOrWhiteSpace(Otp1) || string.IsNullOrWhiteSpace(Otp2) ||
                string.IsNullOrWhiteSpace(Otp3) || string.IsNullOrWhiteSpace(Otp4))
            {
                Application.Current.MainPage.DisplayAlert("Error", "Please enter a valid OTP.", "OK");
                return;
            }

            string enteredOtp = Otp1 + Otp2 + Otp3 + Otp4;
            Application.Current.MainPage.DisplayAlert("OTP Verified", $"You entered: {enteredOtp}", "OK");
        }

        private void ResendOtp()
        {
            countdown = 60;
            IsResendEnabled = false;
            StartCountdown();
        }

        private void StartCountdown()
        {
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += (sender, args) =>
            {
                countdown--;
                ResendOtpText = $"Resend OTP ({countdown}s)";
                if (countdown <= 0)
                {
                    timer.Stop();
                    IsResendEnabled = true;
                    ResendOtpText = "Resend OTP";
                }
            };
            timer.Start();
        }
    }
}
