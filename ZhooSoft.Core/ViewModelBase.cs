using ZhooSoft.Core.NavigationBase;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace ZhooSoft.Core
{
    public partial class ViewModelBase : ObservableObject
    {
        public ICommand BackCommand { get; }

        public Dictionary<string, object> NavigationParams { get; set; } = new Dictionary<string, object>();
        public ICommand OpenFlyout { get; }
        public INavigationService _navigationService { get; set; }

        public IHelperService _helperService { get; set; }

        [ObservableProperty]
        private string pageTitleName;

        public ViewModelBase()
        {
            _navigationService = ServiceHelper.GetService<INavigationService>();
            _helperService = ServiceHelper.GetService<IHelperService>();
            BackCommand = new Command(GoBack);
        }

        private void GoBack(object obj)
        {
            _navigationService.PopAsync();
        }

        private async void OpenFlyoutPanel(object obj)
        {
            //await _navigationService.(new SamplePopup(), true);
        }

        public virtual void OnAppearing()
        {
        }
        public virtual void OnDisappearing()
        {
        }
    }
}
