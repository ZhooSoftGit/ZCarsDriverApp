﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using ZCarsDriver.Resources.Strings;
using ZCarsDriver.Views.Driver;
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

        private void OnTileClicked(string option)
        {
            if (string.IsNullOrEmpty(option))
                return;

            switch (option)
            {
                case "Driver":
                    var page = ServiceHelper.GetService<RegistrationBasePage>();
                    var nvparam = new Dictionary<string, object>
                    {
                        {"Tile", "Driver" }
                    };
                    _navigationService.PushAsync(page, nvparam);
                    break;

                case "Vendor":
                    // Navigate to Vendor Registration Page
                    Shell.Current.GoToAsync("//VendorRegistrationPage");
                    break;

                case "ServiceProvider":
                    // Navigate to Service Provider Registration Page
                    Shell.Current.GoToAsync("//ServiceProviderRegistrationPage");
                    break;

                case "SparParts":
                    // Navigate to Spare Parts Registration Page
                    Shell.Current.GoToAsync("//SparPartsRegistrationPage");
                    break;

                case "BuyAndSell":
                    // Navigate to Buy and Sell Page
                    Shell.Current.GoToAsync("//BuyAndSellPage");
                    break;

                default:
                    // Handle unknown case
                    break;
            }
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

        private void ShowPayment()
        {
        }

        private void ShowRideHistory()
        {
        }

        #endregion
    }
}
