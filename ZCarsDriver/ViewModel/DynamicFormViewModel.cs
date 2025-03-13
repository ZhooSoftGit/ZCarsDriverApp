using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ZCarsDriver.UIModel;
using ZhooSoft.Controls;
using ZhooSoft.Core;

namespace ZCarsDriver.ViewModel
{
    public partial class DynamicFormViewModel : ViewModelBase
    {
        #region Fields

        private CheckListItem _checklistItem;

        [ObservableProperty]
        private ObservableCollection<FormField> _formFields;

        #endregion

        #region Constructors

        public DynamicFormViewModel()
        {
            PageTitleName = "Form";
            SaveCommand = new AsyncRelayCommand(Save);
        }

        #endregion

        #region Properties

        public IAsyncRelayCommand SaveCommand { get; }

        #endregion

        #region Methods

        public override async void OnAppearing()
        {
            base.OnAppearing();
            IsBusy = true;
            if (NavigationParams != null && NavigationParams["checklist"] is CheckListItem item)
            {
                _checklistItem = item;
                PageTitleName = _checklistItem.ItemName;
                LoadFormFields();
            }
            await Task.Delay(100);
            IsBusy = false;
        }

        private void LoadFormFields()
        {
            if (_checklistItem.CheckListType == UIHelper.CheckListType.BasicDetails)
            {
                FormFields = new ObservableCollection<FormField>
                    {
                        new() { Label = "Full Name", Type = FieldType.Text, Placeholder = "Enter full name", IsRequired = true },
                        new() { Label = "Phone Number", Type = FieldType.Number, Placeholder = "Enter phone number", IsRequired = true },
                        new() { Label = "Email Address", Type = FieldType.Text, Placeholder = "Enter email (optional)" },
                        new() { Label = "Gender", Type = FieldType.RadioButton, Options = new List<string> { "Male", "Female", "Other" }, IsRequired = true },
                        new() { Label = "Date of Birth", Type = FieldType.Date, IsRequired = true },
                        new() { Label = "Address", Type = FieldType.Text, Placeholder = "Enter address" }
                    };
            }
            else if (_checklistItem.CheckListType == UIHelper.CheckListType.VehicleDetails)
            {
                var VehicleFormFields = new List<FormField>
                                    {
                                        new FormField { Label = "Vehicle Make/Brand", Type = FieldType.Text, Placeholder = "Enter vehicle brand", IsRequired = true },
                                        new FormField { Label = "Vehicle Model", Type = FieldType.Text, Placeholder = "Enter vehicle model", IsRequired = true },
                                        new FormField { Label = "Vehicle Year of Manufacture", Type = FieldType.Number, Placeholder = "Enter year (e.g., 2020)", IsRequired = true },
                                        new FormField { Label = "Vehicle Color", Type = FieldType.Text, Placeholder = "Enter vehicle color", IsRequired = true },
                                        new FormField { Label = "Vehicle Registration Number (RC No.)", Type = FieldType.Text, Placeholder = "Enter RC number", IsRequired = true },
                                        new FormField { Label = "Chassis Number", Type = FieldType.Text, Placeholder = "Enter chassis number", IsRequired = true },
                                        new FormField { Label = "Engine Number", Type = FieldType.Text, Placeholder = "Enter engine number", IsRequired = true },
                                        new FormField { Label = "Vehicle Type", Type = FieldType.Picker, Options = new List<string> { "Hatchback", "Sedan", "SUV", "MPV", "Luxury", "Bike Taxi" }, IsRequired = true },
                                        new FormField { Label = "Fuel Type", Type = FieldType.Picker, Options = new List<string> { "Petrol", "Diesel", "CNG", "Electric" }, IsRequired = true },
                                        new FormField { Label = "Seating Capacity", Type = FieldType.Number, Placeholder = "Enter seating capacity", IsRequired = true }
                                    };
                FormFields = new ObservableCollection<FormField>(VehicleFormFields);
            }

            else if (_checklistItem.CheckListType == UIHelper.CheckListType.ServiceStationDetails)
            {
                var SSFormFields = new List<FormField>
                                    {
                                        new FormField { Label = "Service Type", Type = FieldType.Picker, Options = new List<string> { "General Service", "Oil Change", "Tire Replacement", "Battery Check", "Brake Inspection" }, IsRequired = true },
                                        new FormField { Label = "Service Details", Type = FieldType.Text, Placeholder = "Enter additional service details", IsRequired = false },
                                        new FormField { Label = "Pickup & Delivery", Type = FieldType.RadioButton, Options = new List<string> { "Yes", "No" }, IsRequired = true },
                                        new FormField { Label = "Free Pickup Distance", Type = FieldType.Number, Placeholder = "KM", IsRequired = true },

                                    };
                FormFields = new ObservableCollection<FormField>(SSFormFields);
            }
            else if (_checklistItem.CheckListType == UIHelper.CheckListType.SpartPartsShopDetails)
            {
                var SparePartsFormFields = new List<FormField>
                                {
                                    new FormField { Label = "Shop Name", Type = FieldType.Text, Placeholder = "Enter shop name", IsRequired = true },
                                    new FormField { Label = "Owner Name", Type = FieldType.Text, Placeholder = "Enter owner's full name", IsRequired = true },
                                    new FormField { Label = "Phone Number", Type = FieldType.Telephone, Placeholder = "Enter contact number", IsRequired = true },
                                    new FormField { Label = "Email Address", Type = FieldType.Email, Placeholder = "Enter email (optional)", IsRequired = false },
                                    new FormField { Label = "Shop Address", Type = FieldType.Text, Placeholder = "Enter full address", IsRequired = true },
                                    new FormField { Label = "Shop Registration Number", Type = FieldType.Text, Placeholder = "Enter registration number", IsRequired = true },
                                    new FormField { Label = "Spare Parts Category", Type = FieldType.Picker, Options = new List<string> { "Engine Parts", "Brakes", "Tires", "Batteries", "Lighting", "Body Parts", "Accessories" }, IsRequired = true },
                                    new FormField { Label = "Available Brands", Type = FieldType.Picker, Options = new List<string> { "Bosch", "Delphi", "Denso", "Mahle", "NGK", "Michelin", "Bridgestone", "Castrol" }, IsRequired = true },
                                    new FormField { Label = "Delivery Option", Type = FieldType.RadioButton, Options = new List<string> { "Yes", "No" }, IsRequired = true },
                                    new FormField { Label = "Online Ordering Available", Type = FieldType.RadioButton, Options = new List<string> { "Yes", "No" }, IsRequired = true },
                                    new FormField { Label = "Payment Methods", Type = FieldType.Picker, Options = new List<string> { "Cash", "Credit Card", "UPI", "Bank Transfer" }, IsRequired = true },
                                    new FormField { Label = "Opening Hours", Type = FieldType.Text, Placeholder = "Enter working hours (e.g., 9 AM - 8 PM)", IsRequired = true },
                                    new FormField { Label = "Additional Notes", Type = FieldType.Text, Placeholder = "Enter any extra details", IsRequired = false }
                                };
                FormFields = new ObservableCollection<FormField>(SparePartsFormFields);
            }
        }

        private async Task Save()
        {
            var isNotValid = FormFields.ToList().Exists(x => x.IsRequired && (string.IsNullOrEmpty(x.Value) && x.DateValue == null));
            if (isNotValid)
            {
                await _alertService.ShowAlert("Validation", "Please fill the data", "Ok");
            }
            else
            {
                //API call
                await _alertService.ShowAlert("Success", $"{_checklistItem.ItemName} is saved", "ok");
                _checklistItem.IsCompleted = true;
                await _navigationService.PopAsync();
            }
        }

        #endregion
    }
}
