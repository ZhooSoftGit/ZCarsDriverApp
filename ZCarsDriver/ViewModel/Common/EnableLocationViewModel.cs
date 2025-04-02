using System.Windows.Input;
using ZhooSoft.Auth;
using ZhooSoft.Core;

namespace ZCarsDriver.ViewModel.Common
{
    public class EnableLocationViewModel : ViewModelBase
    {
        #region Constructors

        public EnableLocationViewModel()
        {
            EnableLocationCommand = new Command(async () => await RequestLocationPermission());
        }

        #endregion

        #region Properties

        public ICommand EnableLocationCommand { get; }

        public ICommand SkipCommand { get; }

        #endregion

        #region Methods

        private async Task RequestLocationPermission()
        {
            var locationPermission = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (locationPermission != PermissionStatus.Granted)
            {
                await _alertService.ShowAlert("Permission Denied", "Please enable location in settings.", "OK");
            }
            else
            {
                try
                {
                    var location = await Geolocation.GetLastKnownLocationAsync() ?? await Geolocation.GetLocationAsync();
                    ServiceHelper.GetService<IMainAppNavigation>().NavigateToMain(true);
                }
                catch (Exception ex)
                {
                    await _alertService.ShowAlert("Permission Denied", "Location service is not running.Please enable it", "OK");
                }
            }
        }

        #endregion
    }
}
