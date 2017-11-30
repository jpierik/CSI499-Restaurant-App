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
using System.Threading.Tasks;

namespace Project16_Mobile.Droid
{
    [Activity(Label = "DashboardActivity", Theme = "@style/CustomActionBarTheme")]
    public class DashboardActivity : AppCompatActivity, ILocationListener
    {
        Context mContext;
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
        Android.Support.V7.App.AlertDialog.Builder mBuilder;
        Android.Support.V7.App.AlertDialog mAlertDialog;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);            
            SetContentView(Resource.Layout.activity_dashboard);

            mContext = this;

            mSelection = (Button)FindViewById(Resource.Id.btnInlineView);
            mSelection.Visibility = ViewStates.Invisible;
            mLocationName = (TextView)FindViewById(Resource.Id.txtInlineName);
            mLocationName.Visibility = ViewStates.Invisible;
            mInfo = (TextView)FindViewById(Resource.Id.txtInlineOther);
            mInfo.Visibility = ViewStates.Invisible;
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
                StartActivity(typeof(InlineActivity));
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
            mUserId = i.GetIntExtra("com.csi4999.inline.EXTRA_USER_ID", -1);
            string fullName = i.GetStringExtra("com.csi4999.inline.EXTRA_USER_FULLNAME");
            string email = i.GetStringExtra("com.csi4999.inline.EXTRA_EMAIL");
            SupportActionBar.Title = "Welcome " + fullName;
            User user = new User();
            user.UserId = mUserId;
            user.FullName = fullName;
            user.email = email;
            library.SetUser(user);

            profile = FindViewById<LinearLayout>(Resource.Id.layoutProfile);
            profile.Click += delegate
            {
                Android.Support.V7.App.AlertDialog.Builder profileBuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
                profileBuilder.SetTitle("User Profile");

                View profileView = LayoutInflater.Inflate(Resource.Layout.activity_profile, null);
                EditText txtName = profileView.FindViewById<EditText>(Resource.Id.txtName);
                txtName.Text = fullName;
                EditText txtEmail = profileView.FindViewById<EditText>(Resource.Id.txtEmail);
                txtEmail.Text = user.email;
                profileBuilder.SetView(profileView);
                profileBuilder.SetPositiveButton("Save", (s, e) => 
                {
                    UpdateUser(user.UserId, txtName.Text, txtEmail.Text);
                    //library.UpdateUser(user.UserId, txtName.Text, txtEmail.Text);
                });
                profileBuilder.SetNegativeButton("Exit", (s, e) => { });
                profileBuilder.Show();
            };
            // Create your application here

            /*
            Address address = ReverseGeocodeCurrentLocation();
            DisplayAddress(address);
            */


        }
        public async void UpdateUser(int id, string name, string email)
        {
            Task<bool> output = library.UpdateUser(id, name, email);
            bool value = await output;
            if (value)
                Toast.MakeText(ApplicationContext, "Profile Updated", ToastLength.Short).Show();
            else
                Toast.MakeText(ApplicationContext, "Please try again later", ToastLength.Short).Show();
        }
        public void DismissDialog()
        {
            mAlertDialog.Dismiss();
            mSelection.Visibility = ViewStates.Invisible;
            mLocationName.Visibility = ViewStates.Invisible;
            mInfo.Visibility = ViewStates.Invisible;
            CheckForWaitingParty();
        }

        public void CheckForWaitingParty()
        {
            List<WaitingParty> list = library.GetWaitingParties();
            if (list == null)
                return;

            WaitingParty party = null;
            foreach (WaitingParty wp in list)
            {
                if (wp.MobileUserId == mUserId)
                    party = wp;
            }
            if (party != null)
            {
                Restaurant restaurant = library.GetRestaurant(party.RestaurantID);
                if (restaurant != null)
                {
                    mSelection.Visibility = ViewStates.Visible;
                    mLocationName.Visibility = ViewStates.Visible;
                    mInfo.Visibility = ViewStates.Visible;
                    mLocationName.Text = restaurant.Name;
                    mInfo.Text = restaurant.Address;
                    mSelection.Click += delegate
                    {
                        mBuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
                        mBuilder.SetTitle("Get InLine");
                        LayoutInflater inflater = this.LayoutInflater;
                        CheckInCheckOutItem item = new CheckInCheckOutItem(mContext, mBuilder.Context, null);
                        item.ID = party.PartyId;
                        item.Address = restaurant.Address;
                        mBuilder.SetView(item);
                        mBuilder.SetCancelable(true);
                        mBuilder.SetNegativeButton("Exit", (s, e) => { mAlertDialog.Dismiss(); });

                        mAlertDialog = mBuilder.Create();
                        mAlertDialog.Show();
                    };
                }
            }
        }
        protected override void OnResume()
        {
            base.OnResume();
            _locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);
            sharedPreferences = GetSharedPreferences("mypref", FileCreationMode.Private);
            mSelection.Visibility = ViewStates.Invisible;
            mLocationName.Visibility = ViewStates.Invisible;
            mInfo.Visibility = ViewStates.Invisible;
            CheckForWaitingParty();         

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
                library.SetCurrentLocation(location.Latitude, location.Longitude);
                //_locationText.Text = string.Format("{0:f6},{1:f6}", _currentLocation.Latitude, _currentLocation.Longitude);
                Address address = ReverseGeocodeCurrentLocation();
                if (address == null)
                    return;
                //DisplayAddress(address);
                long update = sharedPreferences.GetLong("update_weather", 0);
                long now = DateTime.Now.Ticks;
                long sunrise = -1;
                long sunset = -1;
                int weatherId = -1;
                int temperature = -1;
                if (update < now - (TimeSpan.TicksPerMinute * 5))
                {
                    ISharedPreferencesEditor editor = sharedPreferences.Edit();
                    WeatherObject w = library.GetWeather(address.PostalCode);
                    if (w != null)
                    {
                      
                        sunrise = w.sys.sunrise;
                        sunset = w.sys.sunset;
                        weatherId = w.weather[0].id;
                        temperature = w.main.temp;

                        editor.PutLong("update_weather", now);
                        editor.PutInt("weatherId", weatherId);
                        editor.PutInt("temperature", temperature);
                        editor.PutLong("sunrise", sunrise);
                        editor.PutLong("sunset", sunset);
                       
                    }
                    else
                    {
                        editor.PutLong("update_weather", now);
                    }
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
          
                if (weatherId != -1 && sunrise != -1 && sunset != -1 && temperature != -1)
                {
                    setWeatherIcon(weatherId, sunrise, sunset);
                    txtWeather.Text = address.Locality + " " + temperature + " °F";
                }

                DateTime date = DateTime.Now;
                string format = "ddd, dd MMM";
                txtDate.Text = date.ToString(format);
                
            }
        }
        private void setWeatherIcon(int actualId, long sunrise, long sunset)
        {
            int id = actualId / 100;
            string icon = "";
            if (actualId == 800){
               
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
            try
            {
                Geocoder geocoder = new Geocoder(this);
                IList<Address> addressList = geocoder.GetFromLocation(_currentLocation.Latitude, _currentLocation.Longitude, 10);

                Address address = addressList.FirstOrDefault();
                return address;
            }catch(Exception ex)
            {
                return null;
            }
        }
      
    }
}