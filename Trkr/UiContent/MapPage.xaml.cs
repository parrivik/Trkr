using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Trkr.RestServices.DataConstruct;
using Xamarin.Forms.Maps;
using Plugin.Geolocator;
using Xamarin.Essentials;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;





namespace Trkr.UiContent
{
    public partial class MapPage : ContentPage
    {
        private Position _position;
        Trkr.RestServices.RestService _restService;
        public static Thread trackingThread = new Thread(TrackingLoop);

        public MapPage()
        {
            InitializeComponent();


            _restService = new Trkr.RestServices.RestService();

            // create Map to view!
            var map = new Xamarin.Forms.Maps.Map(
                MapSpan.FromCenterAndRadius(
                    // ZHAW Winterthur 47.496848 / 8.729818
                    new Position(47.496848, 8.729818), Distance.FromKilometers(0.45)))
            {
                IsShowingUser = true,
                // note sure if Height and WidthRequest is necessary
                //HeightRequest = 100,
                //WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };


            if (IsLocationAvailable())
            {
                GetPosition();
                //map.MoveToRegion(MapSpan.FromCenterAndRadius(_position, Distance.FromKilometers(0.45)));

            }
            map.MapType = MapType.Street;
            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(map);
            Content = stack;

        }


        public bool IsLocationAvailable()
        {
            if (!CrossGeolocator.IsSupported)
                return false;

            return CrossGeolocator.Current.IsGeolocationAvailable;
        }


        public async void GetPosition()
        {
            Plugin.Geolocator.Abstractions.Position position = null;
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 100;

                position = await locator.GetLastKnownLocationAsync();

                if (position != null)
                {
                    _position = new Position(position.Latitude, position.Longitude);
                    //got a cahched position, so let's use it.
                    return;
                }

                if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
                {
                    //not available or enabled
                    return;
                }

                position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10), null, true);

                // starting of thread
                trackingThread.Start();

                LocationData locationParameter = null;

                if (position != null)
                {

                    //Console.WriteLine($"Latitude: {position.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    locationParameter = new LocationData
                    {
                        Guid = Guid.NewGuid(),
                        Latitude = position.Latitude,
                        Longitude = position.Longitude,
                        Velocity = position.Speed,
                        Timestamp = DateTime.Now.ToString(),
                        UserId = App.logedInUser.Email
                    };

                }
                //UserData userData = await _restService.CreateUser(GenerateUser(Constants.createUser), userToSave);
                //LocationData locationData = await _restService.CreateLocation(GenerateLocationUri(Constants.createPosition), locationParameter);
            }
            catch (Exception ex)
            {
                throw ex;
                //Display error as we have timed out or can't get location.
            }
            _position = new Position(position.Latitude, position.Longitude);
            if (position == null)
                return;
        }


        static async void TrackingLoop()
        {

            Trkr.RestServices.RestService _restService;
            _restService = new Trkr.RestServices.RestService();

            CustomMap iCustomMap = new CustomMap();
            CustomMap havetoListTo = new CustomMap();

            while (true)
            {


                LocationData locationParameter = null;


                try
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Best);
                    Location location = await Geolocation.GetLocationAsync(request);
                    if (location != null)
                    {
                        Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                        locationParameter = new LocationData
                        {
                            Guid = Guid.NewGuid(),
                            Latitude = location.Latitude,
                            Longitude = location.Longitude,
                            Velocity = (double)location.Speed,
                            Timestamp = DateTime.Now.ToString(),
                            UserId = App.logedInUser.Email
                        };

                    }


                    // chechs if i moving faster than 1 m/s^
                    if (locationParameter.Velocity > 1)
                    {
                        LocationData locationData = await _restService.CreateLocation(GenerateLocationUri(Constants.locationAdress), locationParameter);
                        iCustomMap.RouteCoordinates.Add(new Position(locationParameter.Latitude, locationParameter.Longitude));
                        iCustomMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(locationParameter.Latitude, locationParameter.Longitude), Distance.FromKilometers(1.5)));

                        havetoListTo.RouteCoordinates.Add(new Position(locationParameter.Latitude, locationParameter.Longitude));
                    }
                    


                }

                catch (FeatureNotSupportedException fnsEx)
                {
                    // Handle not supported on device exception
                }
                catch (FeatureNotEnabledException fneEx)
                {
                    // Handle not enabled on device exception
                }
                catch (PermissionException pEx)
                {
                    // Handle permission exception
                }
                catch (Exception ex)
                {
                    // Unable to get location
                }

                await Task.Delay(10000);
            }


            string GenerateLocationUri(string endpoint)
            {
                string requestUri = endpoint;

                requestUri += $"?apiKey={Constants.APIKey}";

                return requestUri;

            }


        }
    }
}
