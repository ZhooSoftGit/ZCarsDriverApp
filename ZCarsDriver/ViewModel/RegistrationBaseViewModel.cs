using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZCarsDriver.UIModel;
using ZCarsDriver.Views.Driver;
using ZhooCars.Common;
using ZhooSoft.Core;

namespace ZCarsDriver.ViewModel
{
    public partial class RegistrationBaseViewModel : ViewModelBase
    {
        #region Fields

        [ObservableProperty]
        private ObservableCollection<CheckListItem> _checkListItems;

        [ObservableProperty]
        private bool _isSubmitEnabled;

        private UserRoles _userRole;

        #endregion

        #region Constructors

        public RegistrationBaseViewModel()
        {
            CheckListCmd = new AsyncRelayCommand<CheckListItem>(ToggleItemStatus);
            SubmitApplicationCommand = new RelayCommand(SubmitApplication, CanSubmit);
            PageTitleName = "Registration";
        }

        #endregion

        #region Properties

        public IAsyncRelayCommand CheckListCmd { get; }

        public ICommand SubmitApplicationCommand { get; }

        #endregion

        #region Methods

        public override async void OnAppearing()
        {
            base.OnAppearing();
            IsBusy = true;
            if (NavigationParams.Count > 0)
            {
                if (NavigationParams.ContainsKey("Tile") && NavigationParams["Tile"] is UserRoles role)
                {
                    _userRole = role;
                }

                NavigationParams.Clear();
            }
            LoadCheckList();
            await Task.Delay(100);
            IsBusy = false;
        }

        private bool CanSubmit() => IsSubmitEnabled;

        private void LoadCheckList()
        {
            if (CheckListItems == null)
            {
                var checklist = new List<CheckListItem>
                            {
                                new() { ItemName = "Basic Details", IsCompleted = false, IsForm = true },
                                new() { ItemName = "Profile Photo", IsCompleted = false, IsDocument = true, CheckListType = UIHelper.CheckListType.ProfilePhoto },
                                new() { ItemName = "Aadhaar", IsCompleted = false, IsDocument = true, CheckListType = UIHelper.CheckListType.AadharCard }
                            };
                switch (_userRole)
                {
                    case UserRoles.Driver:
                        checklist.Add(new CheckListItem { ItemName = "Driving License", IsCompleted = false, IsDocument = true, CheckListType = UIHelper.CheckListType.DrivingLicense });
                        break;

                    case UserRoles.Vendor:
                        checklist.Add(new CheckListItem { ItemName = "RC Book", IsCompleted = false, IsDocument = true, CheckListType = UIHelper.CheckListType.RCBook });
                        checklist.Add(new CheckListItem { ItemName = "Vehicle Details", IsCompleted = false, CheckListType = UIHelper.CheckListType.VehicleDetails, IsForm = true });
                        break;

                    case UserRoles.SparPartsDistributor:
                        checklist.Add(new CheckListItem { ItemName = "Service Details", IsCompleted = false, CheckListType = UIHelper.CheckListType.ServiceStationDetails, IsForm = true });
                        checklist.Add(new CheckListItem { ItemName = "GST Document", IsCompleted = false, FrontOnly = true, IsDocument = true, CheckListType = UIHelper.CheckListType.GSTDocument });
                        break;

                    case UserRoles.ServiceProvider:
                        checklist.Add(new CheckListItem { ItemName = "Shop Details", IsCompleted = false, CheckListType = UIHelper.CheckListType.SpartPartsShopDetails, IsForm = true });
                        checklist.Add(new CheckListItem { ItemName = "GST Document", IsCompleted = false, FrontOnly = true, IsDocument = true, CheckListType = UIHelper.CheckListType.GSTDocument });
                        break;

                    default:
                        break;
                }

                CheckListItems = new ObservableCollection<CheckListItem>(checklist);
            }

            UpdateSubmitButtonState();
        }

        private void SubmitApplication()
        {
        }

        private async Task ToggleItemStatus(CheckListItem item)
        {
            if (item != null)
            {
                if (item.IsDocument)
                {
                    var param = new Dictionary<string, object>
                    {
                        {"checklist", item }
                    };
                    await _navigationService.PushAsync(ServiceHelper.GetService<DrivingLicensePage>(), param);
                }

                if (item.IsForm)
                {
                    var param = new Dictionary<string, object>
                    {
                        {"checklist", item }
                    };
                    await _navigationService.PushAsync(ServiceHelper.GetService<CommonFormPage>(), param);
                }

                UpdateSubmitButtonState();
            }
        }

        private void UpdateSubmitButtonState()
        {
            IsSubmitEnabled = CheckListItems.All(item => item.IsCompleted);
        }

        #endregion
    }
}
