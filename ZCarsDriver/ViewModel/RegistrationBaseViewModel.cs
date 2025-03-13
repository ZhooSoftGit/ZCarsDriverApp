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

        public override void OnAppearing()
        {
            base.OnAppearing();

            if (NavigationParams.Count > 0)
            {
                if (NavigationParams.ContainsKey("Tile") && NavigationParams["Tile"] is UserRoles role)
                {
                    _userRole = role;
                }

                if (NavigationParams.ContainsKey("checklist") && NavigationParams["checklist"] is CheckListItem item)
                {
                    var updatedItem = CheckListItems.First(x => x.ItemName == item.ItemName);
                    updatedItem.IsCompleted = item.IsCompleted;
                }

                NavigationParams.Clear();
            }
            LoadCheckList();
        }

        private bool CanSubmit() => IsSubmitEnabled;

        private void LoadCheckList()
        {
            if (CheckListItems == null)
            {
                var checklist = new List<CheckListItem>
                            {
                                new() { ItemName = "Profile Photo", IsCompleted = false, IsDocument = true },
                                new() { ItemName = "Aadhaar", IsCompleted = false, IsDocument = true }
                            };
                switch (_userRole)
                {
                    case UserRoles.Driver:
                        checklist.Add(new CheckListItem { ItemName = "Driving License", IsCompleted = false, IsDocument = true });
                        break;

                    case UserRoles.Vendor:
                        checklist.Add(new CheckListItem { ItemName = "RC Book", IsCompleted = false, IsDocument = true });
                        checklist.Add(new CheckListItem { ItemName = "Vehicle Details", IsCompleted = false });
                        break;

                    case UserRoles.SparPartsDistributor:
                        checklist.Add(new CheckListItem { ItemName = "Shop Details", IsCompleted = false });
                        checklist.Add(new CheckListItem { ItemName = "GST Document", IsCompleted = false, FrontOnly = true, IsDocument = true });
                        break;

                    case UserRoles.ServiceProvider:
                        checklist.Add(new CheckListItem { ItemName = "Shop Details", IsCompleted = false });
                        checklist.Add(new CheckListItem { ItemName = "GST Document", IsCompleted = false, FrontOnly = true, IsDocument = true });
                        break;

                    default:
                        break;
                }
                checklist.Add(new CheckListItem { ItemName = "Local Address", IsCompleted = false });

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
                    await _navigationService.PushAsync(ServiceHelper.GetService<DrivingLicensePage>(), param);
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
