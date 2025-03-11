using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using ZCarsDriver.Resources.Strings;
using ZhooSoft.Core;

namespace ZCarsDriver.ViewModel
{
    public partial class HomeViewModel : ViewModelBase
    {
        #region Fields

        [ObservableProperty]
        private string driverStatus = "Open";

        [ObservableProperty]
        private string driverTileTxt = AppResources.DrivingProfile;

        [ObservableProperty]
        private string profileStatus = "Open";

        [ObservableProperty]
        private string userName = "User Name";

        [ObservableProperty]
        private string userRole = "User Role";

        [ObservableProperty]
        private string userTileTxt = AppResources.UserProfile;

        #endregion

        #region Constructors

        public HomeViewModel()
        {
            ProfileRegCommand = new RelayCommand(ProfileReg);
            DriverRegCommand = new RelayCommand(DriverReg);
            LaunchDashBoardCommand = new RelayCommand(LaunchDashBoard);
            ShowRideHistoryCommand = new RelayCommand(ShowRideHistory);
            ShowPaymentCommand = new RelayCommand(ShowPayment);
            OpenNotificationCommand = new RelayCommand(OpenNotification);
            OpenReferFriendCommand = new RelayCommand(OpenReferFriend);
            OpenSettingsCommand = new RelayCommand(OpenSettings);
            LogoutCommand = new RelayCommand(Logout);

            
        }

        #endregion

        #region Properties

        public ICommand DriverRegCommand { get; }

        public ICommand LaunchDashBoardCommand { get; }

        public ICommand LogoutCommand { get; }

        public ICommand OpenNotificationCommand { get; }

        public ICommand OpenReferFriendCommand { get; }

        public ICommand OpenSettingsCommand { get; }

        public ICommand ProfileRegCommand { get; }

        public ICommand ShowPaymentCommand { get; }

        public ICommand ShowRideHistoryCommand { get; }

        #endregion

        #region Methods

        public override void OnAppearing()
        {
            base.OnAppearing();

            UserTileTxt = AppResources.UserProfile + AppResources.Registration;
            driverTileTxt = AppResources.DrivingProfile + AppResources.Registration;
        }

        private void DriverReg()
        {
        }

        private void LaunchDashBoard()
        {
        }

        private void Logout()
        {
        }

        private void OpenNotification()
        {
        }

        private void OpenReferFriend()
        {
        }

        private void OpenSettings()
        {
        }

        private void ProfileReg()
        {
        }

        private void ShowPayment()
        {
        }

        private void ShowRideHistory()
        {
        }

        #endregion
    }
}
