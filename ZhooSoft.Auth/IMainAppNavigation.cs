using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhooSoft.Auth
{
    public interface IMainAppNavigation
    {
        void NavigateToMain(bool IsInitialLoad = false);

        void NavigateToDetail(NavigationPage detailPage);

        void NavigateToNotification();
    }
}
