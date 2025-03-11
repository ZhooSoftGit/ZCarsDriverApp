using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using ZhooSoft.Core.Alerts;
using ZhooSoft.Core.NavigationBase;

namespace ZhooSoft.Core
{
    public partial class ViewModelBase : ObservableObject
    {
        #region Fields

        [ObservableProperty]
        private string pageTitleName;
        private bool _isBusy;

        #endregion

        #region Constructors

        public ViewModelBase()
        {
            _navigationService = ServiceHelper.GetService<INavigationService>();
            _alertService = ServiceHelper.GetService<IAlertService>();
            BackCommand = new Command(GoBack);
        }

        #endregion

        #region Properties

        public IAlertService _alertService { get; set; }

        public INavigationService _navigationService { get; set; }

        public ICommand BackCommand { get; }

        public Dictionary<string, object> NavigationParams { get; set; } = new Dictionary<string, object>();

        public ICommand OpenFlyout { get; }

        #endregion

        #region Methods

        public virtual void OnAppearing()
        {
        }

        public virtual void OnDisappearing()
        {
        }

        private void GoBack(object obj)
        {
            _navigationService.PopAsync();
        }

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                ShowProgress(_isBusy);

            }
        }

        protected void ShowProgress(bool show)
        {
            if (show)
            {
                ServiceHelper.GetService<IProgressService>().ShowProgress("Please wait");                
            }
            else
            {
                ServiceHelper.GetService<IProgressService>().HideProgress();
            }
        }

        #endregion
    }
}
