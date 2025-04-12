using Android.Content;
using Android.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZCarsDriver.PlatformHelper;

namespace ZCarsDriver.Platforms.Android.Helpers
{
    public class LocationHelperAndroid : ILocationHelper
    {
        public bool IsLocationEnabled()
        {
            var locationManager = (LocationManager)Microsoft.Maui.ApplicationModel.Platform.CurrentActivity.GetSystemService(Context.LocationService);
            return locationManager.IsProviderEnabled(LocationManager.GpsProvider) ||
                   locationManager.IsProviderEnabled(LocationManager.NetworkProvider);
        }
    }
}
