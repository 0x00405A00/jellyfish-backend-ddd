using MobileApp.Handler.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Handler.Device.Sensor
{
    public class GpsHandler : AbstractDeviceActionHandler<Permissions.LocationWhenInUse,Permissions.LocationAlways> 
    {
        private CancellationTokenSource _cancelTokenSource;
        private bool _isCheckingLocation;

        public GpsHandler(Action userRationalAction, Action userDeniedAction, [CallerMemberName] string caller = "") : base(userRationalAction, userDeniedAction, caller)
        {
        }

        public async Task<Location> GetCurrentLocation()
        {
            bool permissions = await AreRequiredPermissionsGranted();
            if (!permissions)
            {
                return null;
            }
            Location location = null;   
            if(!_isCheckingLocation)
            {
                try
                {
                    _isCheckingLocation = true;
                    _cancelTokenSource = new CancellationTokenSource();

                    GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                    location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    _isCheckingLocation = false;
                }
            }
            return location;
        }
        private async Task<Placemark> GetGeocodeReverseData(Location location)
        {
            bool permissions = await AreRequiredPermissionsGranted();
            if (!permissions)
            {
                return null;
            }
            var latitude = location.Latitude;
            var longitude = location.Longitude;
            IEnumerable<Placemark> placemarks = await Geocoding.Default.GetPlacemarksAsync(latitude, longitude);
            Placemark placemark = placemarks?.FirstOrDefault();

            if (placemark != null)
            {
                /*
                    $"AdminArea:       {placemark.AdminArea}\n" +
                    $"CountryCode:     {placemark.CountryCode}\n" +
                    $"CountryName:     {placemark.CountryName}\n" +
                    $"FeatureName:     {placemark.FeatureName}\n" +
                    $"Locality:        {placemark.Locality}\n" +
                    $"PostalCode:      {placemark.PostalCode}\n" +
                    $"SubAdminArea:    {placemark.SubAdminArea}\n" +
                    $"SubLocality:     {placemark.SubLocality}\n" +
                    $"SubThoroughfare: {placemark.SubThoroughfare}\n" +
                    $"Thoroughfare:    {placemark.Thoroughfare}\n";*/
                return placemark;

            }

            return null;
        }
        public override void SetUserDeniedAction()
        {
            UserDeniedAction = () => { };
        }

        public override void SetUserRationalAction()
        {
            UserRationalAction = () => { };
        }

    }
}
