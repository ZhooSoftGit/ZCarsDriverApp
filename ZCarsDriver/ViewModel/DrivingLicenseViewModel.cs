using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using ZCarsDriver.UIModel;
using ZhooSoft.Core;

namespace ZCarsDriver.ViewModel
{
    public partial class DrivingLicenseViewModel : ViewModelBase
    {
        #region Fields

        [ObservableProperty] private string _backFileName;

        [ObservableProperty] private string _backFileSize;

        [ObservableProperty] private ImageSource _backLicenseImage;

        [ObservableProperty] private bool _bothSide;

        private CheckListItem _checklistItem;

        [ObservableProperty] private string _frontFileName;

        [ObservableProperty] private string _frontFileSize;

        [ObservableProperty] private ImageSource _frontLicenseImage;

        [ObservableProperty] private string _instructions;

        [ObservableProperty] private bool _isBackUploaded;

        [ObservableProperty] private bool _isFrontUploaded;

        [ObservableProperty] private bool _isSaveEnabled;

        #endregion

        #region Constructors

        public DrivingLicenseViewModel()
        {
            UploadFrontCommand = new AsyncRelayCommand(UploadFrontAsync);
            UploadBackCommand = new AsyncRelayCommand(UploadBackAsync);
            RemoveFrontCommand = new Command(RemoveFront);
            RemoveBackCommand = new Command(RemoveBack);
            SaveCommand = new AsyncRelayCommand(SaveLicense);
        }

        #endregion

        #region Properties

        public ICommand RemoveBackCommand { get; }

        public ICommand RemoveFrontCommand { get; }

        public IAsyncRelayCommand SaveCommand { get; }

        public IAsyncRelayCommand UploadBackCommand { get; }

        public IAsyncRelayCommand UploadFrontCommand { get; }

        #endregion

        #region Methods

        public override async void OnAppearing()
        {
            base.OnAppearing();
            IsBusy = true;
            if (NavigationParams != null && NavigationParams["checklist"] is CheckListItem item)
            {
                _checklistItem = item;
                UpdatePageData();
                BothSide = !item.FrontOnly;
            }
            await Task.Delay(100);
            IsBusy = false;
        }

        private void RemoveBack()
        {
            BackLicenseImage = null;
            BackFileName = null;
            BackFileSize = null;
            IsBackUploaded = false;
            UpdateSaveButtonState();
        }

        private void RemoveFront()
        {
            FrontLicenseImage = null;
            FrontFileName = null;
            FrontFileSize = null;
            IsFrontUploaded = false;
            UpdateSaveButtonState();
        }

        private async Task SaveLicense()
        {
            if (!IsSaveEnabled) return;
            _checklistItem.IsCompleted = true;
            await _navigationService.PopAsync();
        }

        private void UpdatePageData()
        {
            PageTitleName = _checklistItem.ItemName + " Document";
            Instructions = $"Upload clear pictures of {_checklistItem.ItemName}. \n Documents should be less than 5MB." +
                $"\n Documents should be JPEG, PNG, or PDF format";
        }

        private void UpdateSaveButtonState()
        {
            if (_checklistItem.FrontOnly)
            {
                IsSaveEnabled = IsFrontUploaded;
            }
            else
            {
                IsSaveEnabled = IsFrontUploaded && IsBackUploaded;
            }
        }

        private async Task UploadBackAsync()
        {
            var result = await UIHelper.UIHelper.PickFile();
            if (result != null)
            {
                BackLicenseImage = await UIHelper.UIHelper.GetImageSource(result) ?? BackLicenseImage;
                BackFileName = result.FileName;
                BackFileSize = UIHelper.UIHelper.GetFileSizeAsync(result).ToString() + " kb";
                IsBackUploaded = true;
                UpdateSaveButtonState();
            }
        }

        private async Task UploadFrontAsync()
        {
            var result = await UIHelper.UIHelper.PickFile();
            if (result != null)
            {
                FrontLicenseImage = await UIHelper.UIHelper.GetImageSource(result) ?? FrontLicenseImage;
                FrontFileName = result.FileName;
                FrontFileSize = UIHelper.UIHelper.GetFileSizeAsync(result).ToString() + " kb";
                IsFrontUploaded = true;
                UpdateSaveButtonState();
            }
        }

        #endregion
    }
}
