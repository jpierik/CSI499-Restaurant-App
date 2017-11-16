using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Locations;
using Android.Graphics;
using Java.Util;
using Android.Support.V7.App;


namespace Project16_Mobile.Droid
{
    [Activity(Label = "DashboardActivity", Theme = "@style/Theme.AppCompat.Light")]
    public class DashboardActivity : AppCompatActivity, ILocationListener
    {
        Location _currentLocation;
        LocationManager _locationManager;
        string location;
        string _locationProvider;
        TextView mLocationName = null, mInfo = null;
        Button mSelection = null;
        TextView weatherIcon = null;
        TextView txtDate, txtWeather;
        Typeface weatherFont;
        Handler handler;
        SQLLibrary library;
        LinearLayout search, deals, profile, logout, checkIn, inlineView;
        int mUserId;
        ISharedPreferences sharedPreferences;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_dashboard);
            mSelection = (Button)FindViewById(Resource.Id.btnInlineView);
            mLocationName = (TextView)FindViewById(Resource.Id.txtInlineName);
            mInfo = (TextView)FindViewById(Resource.Id.txtInlineOther);
            inlineView = FindViewById<LinearLayout>(Resource.Id.inlineView);
            weatherIcon = FindViewById<TextView>(Resource.Id.weatherIcon);
            weatherFont = Typeface.CreateFromAsset(Assets, "Fonts/weather.ttf");
            weatherIcon.SetTypeface(weatherFont, TypefaceStyle.Normal);
            txtDate = FindViewById<TextView>(Resource.Id.txtDate);
            txtWeather = FindViewById<TextView>(Resource.Id.txtWeather);
            search = FindViewById<LinearLayout>(Resource.Id.layoutSearch);

            search.Click += delegate
            {
                StartActivity(typeof(SearchActivity));
            };
            deals = FindViewById<LinearLayout>(Resource.Id.layoutDeals);
            deals.Click += delegate
            {
                StartActivity(typeof(DealsActivity));
            };
            checkIn = FindViewById<LinearLayout>(Resource.Id.layoutCheckIn);
            checkIn.Click += delegate
            {

            };
            profile = FindViewById<LinearLayout>(Resource.Id.layoutProfile);
            profile.Click += delegate
            {

            };
            logout = FindViewById<LinearLayout>(Resource.Id.layoutLogout);
            logout.Click += delegate
            {                
                ISharedPreferencesEditor editor = sharedPreferences.Edit();
                editor.PutString("username", null);
                editor.PutString("password", null);
                editor.Apply();
                StartActivity(typeof(LoginActivity));
                Finish();
            };
            library = SQLLibrary.getInstance();
            InitializeLocationManager();

            Intent i = this.Intent;
            int mUserId = i.GetIntExtra("com.csi4999.inline.EXTRA_USER_ID", -1);
            string fullName = i.GetStringExtra("com.csi4999.inline.EXTRA_USER_FULLNAME");
            SupportActionBar.Title = "Welcome " + fullName;

            List<WaitingParty> list = library.GetWaitingParties();
            WaitingParty party = null;
            foreach(WaitingParty wp in list)
            {
                if (wp.MobileUserId == mUserId)
                    party = wp;
            }
            if (party != null)
            {                
                Restaurant restaurant = library.GetRestaurant(party.RestaurantID);
                if (restaurant != null)
                {
                    mLocationName.Text = restaurant.Name;
                    mInfo.Text = restaurant.Address;
                    mSelection.Click += delegate
                    {
                        Intent intent = new Intent(this, typeof(InlineActivity));
                        StartActivity(intent);
                    };
                }

            }
            // Create your application here

            /*
            Address address = ReverseGeocodeCurrentLocation();
            DisplayAddress(address);
            */


        }

        protected override void OnResume()
        {
            base.OnResume();
            _locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);
            sharedPreferences = GetSharedPreferences("mypref", FileCreationMode.Private);
          

        }
        protected override void OnPause()
        {
            base.OnPause();
            _locationManager.RemoveUpdates(this);
        }
        public void OnProviderDisabled(string provider) { }

        public void OnProviderEnabled(string provider) { }

        public void OnStatusChanged(string provider, Availability status, Bundle extras) { }

        public void OnLocationChanged(Location location)
        {
            _currentLocation = location;
            if (_currentLocation == null)
            {
                //_locationText.Text = "Unable to determine your location. Try again in a short while.";
            }
            else
            {
                //_locationText.Text = string.Format("{0:f6},{1:f6}", _currentLocation.Latitude, _currentLocation.Longitude);
                Address address = ReverseGeocodeCurrentLocation();
                //DisplayAddress(address);
                long update = sharedPreferences.GetLong("update_weather", 0);
                long now = DateTime.Now.Ticks;
                long sunrise;
                long sunset;
                int weatherId;
                int temperature;
                if (update < now - (TimeSpan.TicksPerMinute * 10))
                {
                    WeatherObject w = library.GetWeather(address.Locality);
                    sunrise = w.sys.sunrise;
                    sunset = w.sys.sunset;
                    weatherId = w.weather[0].id;
                    temperature = w.main.temp;
                    ISharedPreferencesEditor editor = sharedPreferences.Edit();
                    editor.PutLong("update_weather", now);
                    editor.PutInt("weatherId", weatherId);
                    editor.PutInt("temperature", temperature);
                    editor.PutLong("sunrise", sunrise);
                    editor.PutLong("sunset", sunset);
                    editor.Apply();

                }
                else
                {
                    weatherId = sharedPreferences.GetInt("weatherId", -1);
                    sunrise = sharedPreferences.GetLong("sunrise", -1);
                    sunset = sharedPreferences.GetLong("sunset", -1);
                    temperature = sharedPreferences.GetInt("temperature", -1);
                    // setWeatherIcon(w.weather[0].id, w.sys.sunrise , w.sys.sunset);
                }

                setWeatherIcon(weatherId, sunrise, sunset);
                DateTime date = DateTime.Now;
                string format = "ddd, dd MMM";
                txtDate.Text = date.ToString(format);
                txtWeather.Text = address.Locality + " " + temperature + " °F";
            }
        }
        private void setWeatherIcon(int actualId, long sunrise, long sunset)
        {
            int id = actualId / 100;
            string icon = "";
            if (actualId == 800)            {
               
                long currentTime = new Date().Time;
                if (currentTime >= sunrise && currentTime < sunset)
                {
                    icon = GetString(Resource.String.weather_sunny);
                }
                else
                {
                    icon = GetString(Resource.String.weather_clear_night);
                }
            }
            else
            {
                switch (id)
                {
                    case 2:
                        icon = GetString(Resource.String.weather_thunder);
                        break;
                    case 3:
                        icon = GetString(Resource.String.weather_drizzle);
                        break;
                    case 7:
                        icon = GetString(Resource.String.weather_foggy);
                        break;
                    case 8:
                        icon = GetString(Resource.String.weather_cloudy);
                        break;
                    case 6:
                        icon = GetString(Resource.String.weather_snowy);
                        break;
                    case 5:
                        icon = GetString(Resource.String.weather_rainy);
                        break;
                }
            }
            weatherIcon.Text = icon;
        }
        private void InitializeLocationManager()
        {
            _locationManager = (LocationManager)GetSystemService(LocationService);
            Criteria criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Fine
            };
            IList<string> acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

            if (acceptableLocationProviders.Any())
            {
                _locationProvider = acceptableLocationProviders.First();
            }
            else
            {
                _locationProvider = string.Empty;
            }

        }
        private void DisplayAddress(Address address)
        {
            if (address != null)
            {
                StringBuilder deviceAddress = new StringBuilder();
                for (int i = 0; i < address.MaxAddressLineIndex; i++)
                {
                    deviceAddress.AppendLine(address.GetAddressLine(i));
                }
                // Remove the last comma from the end of the address.
                location = deviceAddress.ToString();
            }
            else
            {
                location = "Unable to determine the address. Try again in a few minutes.";
            }
        }
        private Address ReverseGeocodeCurrentLocation()
        {
            Geocoder geocoder = new Geocoder(this);
            IList<Address> addressList = geocoder.GetFromLocation(_currentLocation.Latitude, _currentLocation.Longitude, 10);

            Address address = addressList.FirstOrDefault();
            return address;
        }
      
    }
}