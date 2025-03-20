using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using System.Windows.Input;
using ZCars.Model.DTOs.DriverApp;
using ZCarsDriver.DPopup;
using ZCarsDriver.Helpers;
using ZCarsDriver.NavigationExtension;
using ZCarsDriver.Services;
using ZhooCars.Common;
using ZhooSoft.Auth;
using ZhooSoft.Controls;
using ZhooSoft.Core;
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace ZCarsDriver.ViewModel
{
    public partial class DriverDashboardViewModel : ViewModelBase
    {
        [ObservableProperty]
        private int _tripCount;

        [ObservableProperty]
        private decimal _earnings;

        [ObservableProperty]
        private bool _isOnline;

        private bool _stopRealTimeUpdate;

        private RideStatus _rideStatus;

        private bool _startTrip;

        private bool _endTrip;

        public CustomMapView CurrentMap;

        public IAsyncRelayCommand ToggleOnlineStatusCommand { get; }

        public IAsyncRelayCommand OnStartPickupCmd { get; }

        public IAsyncRelayCommand OnReachedPickupCmd { get; }

        public IAsyncRelayCommand OnStartTripCmd { get; }

        public IAsyncRelayCommand OnEndTripCmd { get; }

        public IAsyncRelayCommand ShowTripCmd { get; }

        public IAsyncRelayCommand GotoDashboard { get; }

        private OsrmService _osrmService;
        private Location _destination;

        public DriverDashboardViewModel()
        {
            TripCount = 0;
            Earnings = 0.00m;
            IsOnline = false;
            ToggleOnlineStatusCommand = new AsyncRelayCommand(ToggleOnlineStatus);
            OnStartPickupCmd = new AsyncRelayCommand(OnStartPickup);
            OnReachedPickupCmd = new AsyncRelayCommand(OnReachedPickup);
            OnStartTripCmd = new AsyncRelayCommand(OnStartTrip);
            OnEndTripCmd = new AsyncRelayCommand(OnEndTrip);
            ShowTripCmd = new AsyncRelayCommand(OnShowTrip);

            GotoDashboard = new AsyncRelayCommand(OngotoDashboard);
            _osrmService = new OsrmService();
        }

        private async Task OngotoDashboard()
        {
            var result = await _alertService.ShowConfirmation("Info", "Are you Leaving the driver dashboard?", "Ok", "Cancel");

            if (result)
            {
                ServiceHelper.GetService<IMainAppNavigation>().NavigateToMain();
            }
        }

        private async Task OnShowTrip()
        {
            var model = new BookingRequestModel
            {
                BookingType = "Local",
                Fare = "₹ 194",
                DistanceAndPayment = "0.1 Km / Cash",
                PickupLocation = "Muthanampalayam, Tiruppur",
                PickupAddress = "3/21, Muthanampalayam, Tiruppur, Tamil Nadu 641606, India",
                PickupTime = "06 Feb 2024, 07:15 PM",
                DropoffLocation = "Tiruppur Old Bus Stand",
                RemainingBids = 3
            };

            await ServiceHelper.GetService<IAppNavigation>().OpenRidePopup(model, ServiceHelper.GetService<BookingRequestPopup>());

            //var cc = new CustomPopup();
            //(cc.BindingContext as BookingRequestViewModel).BookingRequest = model;

            //await _navigationService.OpenPopup(cc);
        }

        private async Task OnStartTrip()
        {
            _stopRealTimeUpdate = false;
            _startTrip = true;
            _rideStatus = RideStatus.Started;
            double destinationLat = 12.9716; // Example: Bangalore
            double destinationLng = 77.5946;
            _destination = new Location(destinationLat, destinationLng);
            await StartRealTimeTracking(_destination);
        }

        private async Task OnEndTrip()
        {
            _stopRealTimeUpdate = true;
            _rideStatus = RideStatus.Completed;
        }

        private async Task OnReachedPickup()
        {
            _rideStatus = RideStatus.Reached;
            _stopRealTimeUpdate = true;
        }

        private async Task OnStartPickup()
        {
            _stopRealTimeUpdate = false;
            double destinationLat = 12.9716; // Example: Bangalore
            double destinationLng = 77.5946;
            _destination = new Location(destinationLat, destinationLng);
            await StartRealTimeTracking(_destination);
            _rideStatus = RideStatus.Assigned;
        }

        private async Task StartRealTimeTracking(Location destLocation)
        {
            while (!_stopRealTimeUpdate)
            {
                try
                {
                    var location = await Geolocation.GetLastKnownLocationAsync();
                    if (location == null) continue;

                    await PlotRouteOnMap(location.Latitude, location.Longitude, destLocation.Latitude, destLocation.Longitude);

                    await Task.Delay(5000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }


        public async override void OnAppearing()
        {
            base.OnAppearing();
            await GetCurrentRide();
            if (AppHelper.CurrentRide == null)
            {
                InitializeMap();
            }
            else
            {
                await UpdateMapForRide();
            }
        }

        private async Task GetCurrentRide()
        {
            //Create a API to get current Ride
            //Assign to Apphelper
        }

        public async void InitializeMap()
        {
            try
            {
                var location = await GetDriverLocation();
                if (location != null)
                {
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

        public async Task DrawPolyline()
        {
            var endlocation = new Location(11.076375, 76.9997);
            var route = new Polygon
            {
                StrokeColor = Colors.Blue,
                StrokeWidth = 5,
                Geopath =
                    {
                        await GetDriverLocation(), // Start point
                        endlocation, // End point
                    }
            };
            CurrentMap.MapElements.Add(route);
        }

        private async Task ToggleOnlineStatus()
        {
            //API call to on or off online
            await _alertService.ShowAlert("message", "online", "ok");
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

        private async Task UpdateMapForRide()
        {
            var location = await Geolocation.GetLastKnownLocationAsync() ?? await Geolocation.GetLocationAsync();

            if (location != null)
            {
                double userLat = location.Latitude;
                double userLng = location.Longitude;

                double destinationLat = 12.9716; // Example: Bangalore
                double destinationLng = 77.5946;

                await PlotRouteOnMap(userLat, userLng, destinationLat, destinationLng);
            }
        }

        internal async Task RefreshPage()
        {
            if (AppHelper.CurrentRide != null)
            {
                await UpdateMapForRide();
            }
        }
    }
}
