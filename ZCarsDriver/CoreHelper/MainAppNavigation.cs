using ZCarsDriver.Views;
using ZCarsDriver.Views.Common;
using ZhooSoft.Auth;

namespace ZCarsDriver.CoreHelper
{
    public class MainAppNavigation : IMainAppNavigation
    {
        #region Methods

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

                //var appBasePage = new AppBasePage();
                //NavigationPage.SetHasNavigationBar(appBasePage, false);
                //Application.Current.MainPage = new NavigationPage(appBasePage);

                // Application.Current.MainPage = new NavigationPage(new BTMMainPage());
            }
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
                Application.Current.Windows[0].Page = new EnableLocationPage();
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion
    }
}
