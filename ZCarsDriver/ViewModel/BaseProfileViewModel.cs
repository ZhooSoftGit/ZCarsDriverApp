using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZCarsDriver.Helpers;
using ZCarsDriver.UIModel;
using ZCarsDriver.Views.Driver;
using ZhooCars.Common;
using ZhooSoft.Core;

namespace ZCarsDriver.ViewModel
{
    public partial class BaseProfileViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string _profileStatus = "Incomplete";

        [ObservableProperty]
        private bool _isVendor;

        [ObservableProperty]
        private string _applicationStatus = "Pending";
        private MobileModule _currentModule;

        public IAsyncRelayCommand ProfileTapCommand { get; }
        public IAsyncRelayCommand ApplicationTapCommand { get; }

        public IAsyncRelayCommand LaunchDashBoardCommand { get; }
        public IAsyncRelayCommand ResetDataCommand { get; }

        public IAsyncRelayCommand ContactSupportCommand { get; }

        public IAsyncRelayCommand VehiclesCommand { get; }
        public ICommand LogoutCommand { get; }

        public BaseProfileViewModel()
        {
            ProfileTapCommand = new AsyncRelayCommand(OnProfileTapped);
            ApplicationTapCommand = new AsyncRelayCommand(OnApplicationTapped);
            ResetDataCommand = new AsyncRelayCommand(OnResetData);
            LaunchDashBoardCommand = new AsyncRelayCommand(OnLaunchDashBoard);
            ContactSupportCommand = new AsyncRelayCommand(OnContactSupport);
            VehiclesCommand = new AsyncRelayCommand(OnVehiclesCmd);
            LogoutCommand = new RelayCommand(OnLogout);
        }

        private async Task OnVehiclesCmd()
        {
            await _navigationService.PushAsync(ServiceHelper.GetService<VehicleListPage>());
        }

        public override void OnAppearing()
        {
            base.OnAppearing();

            if (AppHelper.CurrentModule is MobileModule module)
            {
                _currentModule = module;

                if (_currentModule == MobileModule.Vendor)
                {
                    IsVendor = true;
                }
            }
        }

        private async Task OnLaunchDashBoard()
        {
            IsBusy = true;
            await _navigationService.PopToRootAsync();
            await _navigationService.PushAsync(ServiceHelper.GetService<DriverDashboardPage>());
            IsBusy = false;
        }

        private async Task OnResetData()
        {
            await _alertService.ShowAlert("info", "your data will reset", "Ok");
        }

        private async Task OnProfileTapped()
        {
            var param = new Dictionary<string, object>
                    {
                        {"checklist", new CheckListItem { ItemName = "Basic Details", IsCompleted = false, IsForm = true } }
                    };
            await _navigationService.PushAsync(ServiceHelper.GetService<CommonFormPage>(), param);
        }

        private async Task OnApplicationTapped()
        {
            var param = new Dictionary<string, object>
                    {
                        { "regType",  GetRegType() }
                    };
            await _navigationService.PushAsync(ServiceHelper.GetService<RegistrationBasePage>(), param);
        }

        private RegsitrationType GetRegType()
        {
            if (_currentModule == MobileModule.Driver)
            {
                return RegsitrationType.DriverApplication;
            }
            if (_currentModule == MobileModule.Vendor)
            {
                return RegsitrationType.VendorApplication;
            }
            if (_currentModule == MobileModule.ServiceProvider)
            {
                return RegsitrationType.ServiceProviderApplication;
            }
            if (_currentModule == MobileModule.SparParts)
            {
                return RegsitrationType.SparPartsApplication;
            }
            return RegsitrationType.BasicDetails;
        }

        private async Task OnContactSupport()
        {
            // Contact Support Logic
            Console.WriteLine("Contacting Support");
        }

        private void OnLogout()
        {
            // Logout Logic
            Console.WriteLine("Logging Out");
        }
    }
}
