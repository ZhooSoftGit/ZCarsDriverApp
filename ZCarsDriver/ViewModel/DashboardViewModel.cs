using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZCarsDriver.UIModel;
using ZCarsDriver.Views.Driver;
using ZCarsDriver.Views.Vendor;
using ZhooSoft.Core;

namespace ZCarsDriver.ViewModel
{
    public partial class DashboardViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<QuickAction> _quickActions;

        public IAsyncRelayCommand<QuickAction> OnActionCmd { get; }

        public DashboardViewModel()
        {
            PageTitleName = "Vendor Dashboard";

            OnActionCmd = new AsyncRelayCommand<QuickAction>(async (obj) => await Onaction(obj));
            QuickActions = new ObservableCollection<QuickAction>
                {
                    new QuickAction { Name = "Vechicles", Icon = "car_icon.png", Action = Helpers.ActionEnum.Vehicles },
                    new QuickAction { Name = "Your Rides", Icon = "car_icon.png", Action = Helpers.ActionEnum.Rides },
                    new QuickAction { Name = "Earnings", Icon = "car_icon.png", Action = Helpers.ActionEnum.Earnings },
                    new QuickAction { Name = "PeekHours", Icon = "car_icon.png", Action = Helpers.ActionEnum.PeekHrs },
                    new QuickAction { Name = "Reports", Icon = "car_icon.png", Action = Helpers.ActionEnum.Reports },
                    new QuickAction { Name = "Suggestions", Icon = "car_icon.png", Action = Helpers.ActionEnum.Others },
                    new QuickAction { Name = "others", Icon = "car_icon.png" },
                    new QuickAction { Name = "others", Icon = "car_icon.png" },
                };
        }

        private async Task Onaction(QuickAction obj)
        {
            if (obj is QuickAction action)
            {
                if (action.Action == Helpers.ActionEnum.Vehicles)
                {
                    await _navigationService.PushAsync(ServiceHelper.GetService<VehicleListPage>());
                }
                else if (action.Action == Helpers.ActionEnum.Rides)
                {
                    await _navigationService.PushAsync(ServiceHelper.GetService<RideListPage>());
                }
                else if (action.Action == Helpers.ActionEnum.Earnings)
                {
                    await _navigationService.PushAsync(ServiceHelper.GetService<EarningsPage>());
                }
            }
        }
    }
}
