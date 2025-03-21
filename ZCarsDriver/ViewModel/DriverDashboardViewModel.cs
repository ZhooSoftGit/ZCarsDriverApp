using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using System.ComponentModel;
using ZCars.Model.DTOs.DriverApp;
using ZCarsDriver.DPopup;
using ZCarsDriver.Helpers;
using ZCarsDriver.NavigationExtension;
using ZCarsDriver.Services;
using ZCarsDriver.Views.Driver;
using ZhooCars.Common;
using ZhooCars.Model.DTOs;
using ZhooSoft.Auth;
using ZhooSoft.Controls;
using ZhooSoft.Core;

namespace ZCarsDriver.ViewModel
{
    public partial class DriverDashboardViewModel : ViewModelBase
    {
        #region Fields

        public CustomMapView CurrentMap;

        [ObservableProperty]
        private string _actionText = "Reached";

        [ObservableProperty]
        private BookingRequestModel _bookingRequestModel;

        private Location _destination;

        [ObservableProperty]
        private decimal _earnings;

        private bool _endTrip;

        [ObservableProperty]
        private bool _isOnline;

        [ObservableProperty]
        private bool _onTrip;

        private OsrmService _osrmService;

        [ObservableProperty]
        private RideStatus _rideStatus;

        [ObservableProperty]
        private RideTripDto _rideTrip;

        private bool _startRealTimeUpdate;

        private bool _startTrip;

        [ObservableProperty]
        private int _tripCount;

        #endregion

        #region Constructors

        public DriverDashboardViewModel()
        {
            TripCount = 0;
            Earnings = 0.00m;
            IsOnline = false;
            ToggleOnlineStatusCommand = new AsyncRelayCommand(ToggleOnlineStatus);

            ShowTripCmd = new AsyncRelayCommand(OnShowTrip);
            GotoDashboard = new AsyncRelayCommand(OngotoDashboard);
            OpenRideOptionCmd = new AsyncRelayCommand(OpenRideOption);

            OpenCallCommand = new AsyncRelayCommand(OpenCall);
            ToggleDropdownCommand = new RelayCommand(ToggleDropdown);
            CurrentLocCommand = new AsyncRelayCommand(ShowCurrentLocation);
            OnTripAction = new AsyncRelayCommand(HandleTripAction);

            _osrmService = new OsrmService();
        }

        #endregion

        #region Properties

        public IAsyncRelayCommand CurrentLocCommand { get; }

        public IAsyncRelayCommand GotoDashboard { get; }

        public IAsyncRelayCommand OnTripAction { get; }

        public IAsyncRelayCommand OpenCallCommand { get; }

        public IAsyncRelayCommand OpenRideOptionCmd { get; }

        public IAsyncRelayCommand ShowTripCmd { get; }

        public IRelayCommand ToggleDropdownCommand { get; }

        public IAsyncRelayCommand ToggleOnlineStatusCommand { get; }

        #endregion

        #region Methods

        public async void InitializeMap()
        {
            try
            {
                StopRealTimeTracking();
                OnTrip = false;
                var location = await GetDriverLocation();
                if (location != null)
                {
                    CurrentMap.MapElements.Clear();
                    CurrentMap.Pins.Clear();
                    var position = new Location(location.Latitude, location.Longitude);
                    var pin = new CustomPin
                    {
                        Label = "Your Location",
                        Type = PinType.Place,
                        Location = position,
                        Address = "Location",
                        ImageSource = "car_icon.png"
                    };

                    CurrentMap.Pins.Add(pin);

                    CurrentMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(1)));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting location: {ex.Message}");
            }
        }

        public async override void OnAppearing()
        {
            base.OnAppearing();
            await GetCurrentRide();
            if (AppHelper.CurrentRide == null)
            {
                StopRealTimeTracking();
                InitializeMap();
            }
            else
            {
                await RefreshPage();
            }
        }

        public async Task<double?> PlotRouteOnMap(double startLat, double startLng, double endLat, double endLng)
        {
            try
            {
                // Get encoded polyline from OSRM
                var result = await _osrmService.GetEncodedPolylineAsync(startLat, startLng, endLat, endLng);

                if (string.IsNullOrEmpty(result.EncodedPolyline))
                {
                    Console.WriteLine("No route found!");
                    return null;
                }

                // Decode the polyline
                var routePoints = PolylineDecoder.DecodePolyline(result.EncodedPolyline);

                if (routePoints == null || routePoints.Count == 0)
                {
                    Console.WriteLine("Failed to decode polyline.");
                    return null;
                }

                // Clear previous routes
                CurrentMap.MapElements.Clear();

                // Draw the polyline
                var polyline = new Polyline
                {
                    StrokeColor = Colors.Green,
                    StrokeWidth = 5
                };

                foreach (var (lat, lng) in routePoints)
                {
                    polyline.Geopath.Add(new Location(lat, lng));
                }

                CurrentMap.MapElements.Add(polyline);

                // Center the map
                CurrentMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Location(startLat, startLng), Distance.FromKilometers(10)));
                return result.Distance;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error plotting route: {ex.Message}");
            }
            return null;
        }

        internal async Task RefreshPage()
        {
            if (AppHelper.CurrentRide != null && AppHelper.CurrentRide.RideStatus == RideStatus.Assigned)
            {
                RideStatus = RideStatus.Assigned;
                OnTrip = true;
                await OnStartPickup();
            }
            else if (AppHelper.CurrentRide != null && AppHelper.CurrentRide.RideStatus == RideStatus.Started)
            {
                OnTrip = true;
                ActionText = "End Trip";
                await OnStartTrip();
            }
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.PropertyName == nameof(RideStatus))
            {
                switch (RideStatus)
                {
                    case RideStatus.Assigned:
                        {
                            ActionText = "Reached";
                        }
                        break;
                    case RideStatus.Reached:
                        {
                            ActionText = "Started";
                        }
                        break;
                    case RideStatus.Started:
                        {
                            ActionText = "Completed";
                        }
                        break;
                    case RideStatus.Completed:
                        {

                        }
                        break;
                    case RideStatus.Cancelled:
                        {

                        }
                        break;
                }
            }
        }

        private async void Geolocation_LocationChanged(object? sender, GeolocationLocationChangedEventArgs e)
        {
            await PlotRouteOnMap(e.Location.Latitude, e.Location.Longitude, _destination.Latitude, _destination.Longitude);
        }

        private async Task GetCurrentRide()
        {
        }

        private async Task<Location> GetDriverLocation()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync() ?? await Geolocation.GetLocationAsync();
                return location;
            }
            catch (Exception ex)
            {
                await _alertService.ShowAlert("Error", "Enable Location", "Ok");
            }
            return null;
        }

        private async Task HandleTripAction()
        {
            if (AppHelper.CurrentRide.RideStatus == RideStatus.Assigned)
            {
                OnTrip = true;
                RideStatus = RideStatus.Reached;
                AppHelper.CurrentRide.RideStatus = RideStatus.Reached;
                await OnReachedPickup();
            }
            else if (AppHelper.CurrentRide.RideStatus == RideStatus.Reached)
            {
                OnTrip = true;
                RideStatus = RideStatus.Started;
                AppHelper.CurrentRide.RideStatus = RideStatus.Started;
                await OnStartPickup();
            }
            else if (AppHelper.CurrentRide.RideStatus == RideStatus.Started)
            {
                OnTrip = false;
                RideStatus = RideStatus.Completed;
                AppHelper.CurrentRide.RideStatus = RideStatus.Completed;
                await OnEndTrip();
            }
        }

        private async Task OnEndTrip()
        {
            StopRealTimeTracking();
            _rideStatus = RideStatus.Completed;
            OnTrip = false;
            AppHelper.CurrentRide = null;
        }

        private async Task OngotoDashboard()
        {
            var result = await _alertService.ShowConfirmation("Info", "Are you Leaving the driver dashboard?", "Ok", "Cancel");

            if (result)
            {
                ServiceHelper.GetService<IMainAppNavigation>().NavigateToMain();
            }
        }

        private async Task OnReachedPickup()
        {
            _rideStatus = RideStatus.Reached;
            StopRealTimeTracking();
        }

        private async Task OnShowTrip()
        {
        }

        private async Task OnStartPickup()
        {
            _startRealTimeUpdate = true;
            _destination = new Location(AppHelper.CurrentRide.BookingRequest.PickupLatitude, AppHelper.CurrentRide.BookingRequest.PickupLongitude);
            _rideStatus = RideStatus.Assigned;
            OnTrip = true;
            await StartRealTimeTracking();
        }

        private async Task OnStartTrip()
        {
            _startRealTimeUpdate = true;
            _startTrip = true;
            _rideStatus = RideStatus.Started;
            OnTrip = true;

            _destination = new Location(AppHelper.CurrentRide.BookingRequest.DropLatitude, AppHelper.CurrentRide.BookingRequest.DropLongitude);

            await StartRealTimeTracking();
        }

        private async Task OpenCall()
        {
            // Logic to open phone call
            await _alertService.ShowAlert("message", "call is going", "ok");
        }

        private async Task OpenRideOption()
        {
            var SelectedOption = await _navigationService.OpenPopup(new TripOptionPopup());

            if (SelectedOption is PopupEnum result)
            {
                if (result == PopupEnum.TripDetails)
                {
                    await _navigationService.PushAsync(ServiceHelper.GetService<TripDetailsPage>());
                }
                if (result == PopupEnum.CallRide)
                {
                    await _alertService.ShowAlert("message", "call", "Ok");
                }
                if (result == PopupEnum.CancelRide)
                {
                    await _navigationService.PushAsync(ServiceHelper.GetService<CancelTripPage>());
                }
            }
        }

        private async Task ShowCurrentLocation()
        {
            var location = await GetDriverLocation();
            if (location != null)
            {
                CurrentMap.MoveToRegion(MapSpan.FromCenterAndRadius(location, Distance.FromKilometers(1)));
            }
        }

        private async Task StartRealTimeTracking()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();

            if (location != null)
            {
                await PlotRouteOnMap(location.Latitude, location.Longitude, _destination.Latitude, _destination.Longitude);
                Geolocation.LocationChanged -= Geolocation_LocationChanged;
                Geolocation.LocationChanged += Geolocation_LocationChanged;
            }
        }

        private void StopRealTimeTracking()
        {
            _startRealTimeUpdate = false;
            Geolocation.LocationChanged -= Geolocation_LocationChanged;
        }

        private void ToggleDropdown()
        {
            // Logic to show/hide dropdown
            Console.WriteLine("Dropdown toggled");
        }

        private async Task ToggleOnlineStatus()
        {
            //API call to on or off online
            await _alertService.ShowAlert("message", "online", "ok");

            var model = new BookingRequestModel
            {
                BookingType = RideTypeEnum.Local,
                Fare = "₹ 194",
                DistanceAndPayment = "0.1 Km / Cash",
                PickupLocation = "Muthanampalayam, Tiruppur",
                PickupAddress = "3/21, Muthanampalayam, Tiruppur, Tamil Nadu 641606, India",
                PickupLatitude = 11.0176,
                PickupLongitude = 76.9674,
                PickupTime = "06 Feb 2024, 07:15 PM",
                DropoffLocation = "Tiruppur Old Bus Stand",
                DropLatitude = 10.9902,
                DropLongitude = 76.9629,
                RemainingBids = 3
            };

            await ServiceHelper.GetService<IAppNavigation>().OpenRidePopup(model, ServiceHelper.GetService<BookingRequestPopup>());
        }

        #endregion
    }
}
