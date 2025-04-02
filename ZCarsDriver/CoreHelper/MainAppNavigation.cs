using System.Net.Http.Headers;
using ZCarsDriver.Services.Session;
using ZCarsDriver.Views;
using ZCarsDriver.Views.Common;
using ZhooSoft.Auth;
using ZhooSoft.Auth.Model;
using ZhooSoft.Auth.Views;
using ZhooSoft.Core;
using ZhooSoft.Core.Alerts;
using ZhooSoft.Core.Session;

namespace ZCarsDriver.CoreHelper
{
    public class MainAppNavigation : IMainAppNavigation
    {
        private readonly IUserSessionManager? _userSession;
        #region Methods

        public MainAppNavigation()
        {
            _userSession = ServiceHelper.GetService<IUserSessionManager>();
        }

        public void NavigateToDetail(NavigationPage detailPage)
        {
            throw new NotImplementedException();
        }

        public async void NavigateToMain(bool IsInitialLoad = false)
        {
            if (IsInitialLoad)
            {
                if (await CheckPermission())
                {
                    Application.Current.Windows[0].Page = new NavigationPage(new HomeViewPage());
                }
                else
                {
                    Application.Current.Windows[0].Page = new EnableLocationPage();
                }
            }
            else
            {
                Application.Current.Windows[0].Page = new NavigationPage(new HomeViewPage());
            }
        }

        public async Task Initialize()
        {
            var session = await _userSession.GetUserSessionAsync();

            if (session != null)
            {
                UpdateUserDetails(session);
                if (await CheckPermission())
                {
                    Application.Current.Windows[0].Page = new NavigationPage(new HomeViewPage());
                }
                else
                {
                    Application.Current.Windows[0].Page = new EnableLocationPage();
                }
            }
            else
            {
                Application.Current.Windows[0].Page = new NavigationPage(new LoginPage());
            }
        }

        private void UpdateUserDetails(UserSession session)
        {
            UserDetails.getInstance().Phone1 = session.PhoneNumber;
            UserDetails.getInstance().UserRoles = new List<ZhooCars.Common.UserRoles> { ZhooCars.Common.UserRoles.User };
        }

        public void NavigateToNotification()
        {
            throw new NotImplementedException();
        }

        private async Task<bool> CheckPermission()
        {
            var locationPermission = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (locationPermission != PermissionStatus.Granted)
            {
                return false;
            }
            else
            {
                try
                {
                    var location = await Geolocation.GetLastKnownLocationAsync() ?? await Geolocation.GetLocationAsync();
                }
                catch (Exception ex)
                {
                    return false;
                }
                return true;
            }
        }

        #endregion
    }
}
