
using ZhooSoft.Auth;

namespace ZCarsDriver.CoreHelper
{
    public class MainAppNavigation : IMainAppNavigation
    {
        public void NavigateToDetail(NavigationPage detailPage)
        {
            throw new NotImplementedException();
        }

        public void NavigateToMain(bool IsInitialLoad = false)
        {
            if (IsInitialLoad)
            {
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
    }
}
