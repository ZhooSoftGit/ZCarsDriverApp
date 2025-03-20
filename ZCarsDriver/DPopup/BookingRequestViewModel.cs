using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Timers;
using System.Windows.Input;
using ZCars.Model.DTOs.DriverApp;
using ZhooSoft.Core;

namespace ZCarsDriver.DPopup
{
    public partial class BookingRequestViewModel : ViewModelBase
    {
        #region Constants

        private const int TotalTime = 10;

        #endregion

        #region Fields

        [ObservableProperty]
        private BookingRequestModel _bookingRequest;

        [ObservableProperty]
        private double _progressValue;

        private System.Timers.Timer _timer;

        #endregion

        #region Constructors

        public BookingRequestViewModel()
        {
            AcceptCommand = new Command(OnAccept);
            RejectCommand = new Command(OnReject);
            InitiateTimer();
        }

        #endregion

        #region Properties

        public ICommand AcceptCommand { get; }

        public Popup CurrentPopup { get; set; }

        public ICommand RejectCommand { get; }

        public int TimerValue { get; set; }

        #endregion

        #region Methods

        public override void OnNavigatedTo()
        {
            if (NavigationParams != null && NavigationParams.ContainsKey("RequestModel"))
            {
                BookingRequest = NavigationParams["RequestModel"] as BookingRequestModel;
            }
        }

        private void InitiateTimer()
        {
            TimerValue = TotalTime;
            ProgressValue = TimerValue;
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += OnTimerElapsed;
            _timer.Start();
        }

        private void OnAccept()
        {
            // Perform logic for accepting the ride
            Application.Current.MainPage.DisplayAlert("Success", "Ride Accepted", "OK");
        }

        private void OnReject()
        {
            // Perform logic for rejecting the ride
            Application.Current.MainPage.DisplayAlert("Info", "Ride Rejected", "OK");
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            TimerValue--;
            ProgressValue = TimerValue;
            OnPropertyChanged(nameof(TimerValue));
            OnPropertyChanged(nameof(ProgressValue));

            if (TimerValue <= 0)
            {
                _timer.Stop();
                CurrentPopup.Close();
            }
        }

        #endregion
    }
}
