using System.Windows.Input;
using ZhooSoft.Auth;
using ZhooSoft.Core;

namespace ZCarsDriver.ViewModel.Common
{
    public class EnableLocationViewModel : ViewModelBase
    {
        public ICommand EnableLocationCommand { get; }
        public ICommand SkipCommand { get; }

        public EnableLocationViewModel()
        {
            EnableLocationCommand = new Command(async () => await RequestLocationPermission());
        }

        private async Task RequestLocationPermission()
        {
            var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            if (status == PermissionStatus.Granted)
            {
                ServiceHelper.GetService<IMainAppNavigation>().NavigateToMain(true);
            }
            else
            {
                await Shell.Current.DisplayAlert("Permission Denied", "Please enable location in settings.", "OK");
            }
        }
    }
}
