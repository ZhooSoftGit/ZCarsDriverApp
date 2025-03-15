using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZCarsDriver.UIModel;
using ZhooSoft.Core;

namespace ZCarsDriver.ViewModel
{
    public partial class LinkDriverViewModel :ViewModelBase
    {
        private List<Cab> AvailableCabs;

        [ObservableProperty]
        private ObservableCollection<Cab> filteredCabs;

        [ObservableProperty]
        private string searchText;

        [ObservableProperty]
        private bool hasRcBook;

        [ObservableProperty]
        private Cab selectedCab;

        public bool CanContinue => SelectedCab != null;

        public ICommand SelectCabCommand { get; }
        public ICommand ContinueCommand { get; }
        public ICommand ChangeVendorCommand { get; }
        public ICommand SearchCommand { get; set; }

        public LinkDriverViewModel()
        {
            SelectCabCommand = new RelayCommand<Cab>(SelectCab);
            ContinueCommand = new RelayCommand(OnContinue, () => CanContinue);
            ChangeVendorCommand = new RelayCommand(OnChangeVendor);
            SearchCommand = new RelayCommand(FilterCabs);

        }

        public override void OnAppearing()
        {
            base.OnAppearing();

            AvailableCabs = new List<Cab>
            {
                new Cab { RegistrationNumber = "TN00000000", Model = "White Aspire" },
                new Cab { RegistrationNumber = "KA12345678", Model = "Black Sedan" },
                new Cab { RegistrationNumber = "MH87654321", Model = "Silver SUV" },
            };

            FilteredCabs = new ObservableCollection<Cab>(AvailableCabs);
        }



        private void SelectCab(Cab cab)
        {
            if (cab == null) return;

            foreach (var c in FilteredCabs)
                c.IsSelected = false;

            SelectedCab.IsSelected = true;
        }

        private void OnContinue()
        {
            if (SelectedCab != null)
            {
                Application.Current.MainPage.DisplayAlert("Cab Selected",
                    $"You selected {SelectedCab.RegistrationNumber}", "OK");
            }
        }

        private void OnChangeVendor()
        {
            Application.Current.MainPage.DisplayAlert("Change Vendor",
                "Vendor change process initiated.", "OK");
        }

        private void FilterCabs()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredCabs = new ObservableCollection<Cab>(AvailableCabs);
            }
            else
            {
                var filtered = AvailableCabs.Where(c => c.RegistrationNumber.Contains(SearchText) ||
                                               c.Model.Contains(SearchText)).ToList();
                FilteredCabs = new ObservableCollection<Cab>(filtered);
            }
            OnPropertyChanged(nameof(FilteredCabs));
        }
    }
}
