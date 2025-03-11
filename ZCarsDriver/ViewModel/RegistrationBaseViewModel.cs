using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZCarsDriver.UIModel;
using ZhooSoft.Core;

namespace ZCarsDriver.ViewModel
{
    public partial class RegistrationBaseViewModel : ViewModelBase
    {

        [ObservableProperty]
        private ObservableCollection<CheckListItem> _checkListItems;

        [ObservableProperty]
        private bool _isSubmitEnabled;

        public ICommand CheckListCmd { get; }
        public ICommand SubmitApplicationCommand { get; }

        public RegistrationBaseViewModel()
        {
            CheckListCmd = new RelayCommand<CheckListItem>(ToggleItemStatus);
            SubmitApplicationCommand = new RelayCommand(SubmitApplication, CanSubmit);
            LoadCheckList();
            PageTitleName = "Registration";
        }

        private void LoadCheckList()
        {
            CheckListItems = new ObservableCollection<CheckListItem>
            {
                new CheckListItem { ItemName = "Profile Photo", IsCompleted = false },
                new CheckListItem { ItemName = "Aadhaar", IsCompleted = false },
                new CheckListItem { ItemName = "Driving License", IsCompleted = false },
                new CheckListItem { ItemName = "Local Address", IsCompleted = false }
            };

            UpdateSubmitButtonState();
        }

        private void ToggleItemStatus(CheckListItem item)
        {
            if (item != null)
            {
                item.IsCompleted = !item.IsCompleted;
                UpdateSubmitButtonState();
            }
        }

        private void UpdateSubmitButtonState()
        {
            IsSubmitEnabled = CheckListItems.All(item => item.IsCompleted);
        }

        private bool CanSubmit() => IsSubmitEnabled;

        private void SubmitApplication()
        {
            // Handle submission logic
        }

        private void GoBack()
        {
            // Handle navigation logic
        }
    }
}
