﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using ZCarsDriver.Views.Driver;
using ZhooCars.Common;
using ZhooSoft.Core;

namespace ZCarsDriver.ViewModel
{
    public partial class HomeViewModel : ViewModelBase
    {
        #region Fields

        [ObservableProperty]
        private string userName = "User Name";

        [ObservableProperty]
        private string userRole = "User Role";

        #endregion

        #region Constructors

        public HomeViewModel()
        {
            ShowRideHistoryCommand = new RelayCommand(ShowRideHistory);
            ShowPaymentCommand = new RelayCommand(ShowPayment);
            OpenNotificationCommand = new RelayCommand(OpenNotification);
            OpenReferFriendCommand = new RelayCommand(OpenReferFriend);
            OpenSettingsCommand = new RelayCommand(OpenSettings);
            LogoutCommand = new RelayCommand(Logout);

            TileClickCmd = new RelayCommand<string>(OnTileClicked);
        }

        #endregion

        #region Properties

        public ICommand LogoutCommand { get; }

        public ICommand OpenNotificationCommand { get; }

        public ICommand OpenReferFriendCommand { get; }

        public ICommand OpenSettingsCommand { get; }

        public ICommand ShowPaymentCommand { get; }

        public ICommand ShowRideHistoryCommand { get; }

        public ICommand TileClickCmd { get; }

        #endregion

        #region Methods

        public override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void Logout()
        {
        }

        private async void OnTileClicked(string option)
        {
            if (string.IsNullOrEmpty(option))
                return;
            option = option.Replace(" ", "");
            switch (option)
            {
                case "Driver":
                    var page = ServiceHelper.GetService<RegistrationBasePage>();
                    var nvparam = new Dictionary<string, object>
                    {
                        {"Tile", UserRoles.Driver }
                    };
                    await _navigationService.PushAsync(page, nvparam);
                    break;

                case "Vendor":
                    page = ServiceHelper.GetService<RegistrationBasePage>();
                    nvparam = new Dictionary<string, object>
                    {
                        {"Tile", UserRoles.Vendor }
                    };
                    await _navigationService.PushAsync(page, nvparam);
                    break;

                case "ServiceProvider":
                    page = ServiceHelper.GetService<RegistrationBasePage>();
                    nvparam = new Dictionary<string, object>
                    {
                        {"Tile", UserRoles.ServiceProvider }
                    };
                    await _navigationService.PushAsync(page, nvparam);
                    break;

                case "SparParts":
                    page = ServiceHelper.GetService<RegistrationBasePage>();
                    nvparam = new Dictionary<string, object>
                    {
                        {"Tile", UserRoles.SparPartsDistributor }
                    };
                    await _navigationService.PushAsync(page, nvparam);
                    break;

                case "BuyAndSell":
                    page = ServiceHelper.GetService<RegistrationBasePage>();
                    nvparam = new Dictionary<string, object>
                    {
                        {"Tile", UserRoles.BuyAndSell }
                    };
                    await _navigationService.PushAsync(page, nvparam);
                    break;

                default:
                    // Handle unknown case
                    break;
            }
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

        private void ShowPayment()
        {
        }

        private void ShowRideHistory()
        {
        }

        #endregion
    }
}
