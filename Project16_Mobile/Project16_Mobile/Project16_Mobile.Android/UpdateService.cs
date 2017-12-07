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
using Java.Util;
using Android.Util;
using Android.Locations;

namespace Project16_Mobile.Droid
{
    [Service]
    public class UpdateService : Service
    {
        public static string EXTRA_RID = "com.csi4999.project16.EXTRA_RID";
        public static string ACTION_UPDATE = "com.csi4999.project16.ACTION_UPDATE";
        public static string EXTRA_RESTAURANT = "com.csi4999.project16.EXTRA_RESTAURANT";
        public static string EXTRA_WAITTIME = "com.csi4999.project16.EXTRA_WAITTIME";
        public static string EXTRA_TABLES = "com.csi.project16.EXTRA_TABLES";
        public static string EXTRA_RNAME = "com.csi.project16.EXTRA_RNAME";
        public static string EXTRA_DEALS_ID = "com.csi.project16.EXTRA_ID";

        public static string EXTRA_ADDRESS = "com.csi.project16.EXTRA_ADDRESS";
        public static string EXTRA_DISTANCE = "com.csi.project16.EXTRA_DISTANCE";

        public IBinder Binder { get; private set; }
        private Timer mUpateTimer;
        private Timer mUpdateWeather;
        public List<ListItem> mResturantList;

        //DisplayAddress(address);
        long mWeatherUpdateTime;//sharedPreferences.GetLong("update_weather", 0);
        long mCurrentTime; // = DateTime.Now.Ticks;
        long mSunrise = -1;
        long mSunset = -1;
        int mWeatherId = -1;
        int mTemperature = -1;
        Address mCurrentAddress;

        SQLLibrary library;

        /*
        ISharedPreferences sharedPreferences;
        Location _currentLocation;
        LocationManager _locationManager;
        string location;
        string _locationProvider;

    */
        public override void OnCreate()
        {
            // This method is optional to implement
            base.OnCreate();
            library = SQLLibrary.getInstance();
           // sharedPreferences = GetSharedPreferences("mypref", FileCreationMode.Private);
            mResturantList = new List<ListItem>();
          //  InitializeLocationManager();
         //   startUpdateTimer();
         //   startUpdateWeather();

        }

        public override IBinder OnBind(Intent intent)
        {
            // This method must always be implemented        
            this.Binder = new LocalBinder(this);
           
            Console.WriteLine("Service Bounded: " + Binder);
            return this.Binder;
        }

        public override bool OnUnbind(Intent intent)
        {
            // This method is optional to implement
       
            return base.OnUnbind(intent);
        }

        public override void OnDestroy()
        {
            // This method is optional to implement
           
            base.OnDestroy();
            stopUpdateTimer();
        //    stopUpdateWeather();
        }

        public class LocalBinder : Binder
        {
            public LocalBinder(UpdateService service)
            {
                this.Service = service;
            }
            public UpdateService Service { get; private set; }
        }
        /*
        public void startUpdateWeather()
        {
            if(mUpdateWeather == null)
            {
                mUpdateWeather = new Timer();
            }
            mUpdateWeather.ScheduleAtFixedRate(new UpdateWeatherTask(this), 0, 5 * 60 * 1000);
        }
        public void stopUpdateWeather()
        {
            if (mUpdateWeather == null)
            {
                return;
            }
            mUpdateWeather.Dispose();
        }
        */
        public void startUpdateTimer()
        {
            if(mUpateTimer == null)
            {
                mUpateTimer = new Timer();
            }
            mUpateTimer.ScheduleAtFixedRate(new UpdateTimerTask(this),0,30000);
        }
        public void stopUpdateTimer()
        {
            if(mUpateTimer == null)
            {
                return;
            }
            mUpateTimer.Dispose();
        }
        public bool doesExist(int id)
        {
            if (mResturantList.Exists(item => item.Index == id)){
                return true;
            }
            return false;
        }
        public void addListItem(ListItem item)
        {
            mResturantList.Add(item);
        }
        public List<ListItem> getResturantList()
        {
            return mResturantList;
        }
        public Address GetLocationAddress()
        {
            return mCurrentAddress;
        }
        /*
        public ISharedPreferences GetSharedPreferences()
        {
            return sharedPreferences;
        }
        public void OnProviderDisabled(string provider) { }

        public void OnProviderEnabled(string provider) { }

        public void OnStatusChanged(string provider, Availability status, Bundle extras) { }

        public void OnLocationChanged(Location location)
        {
            if (_currentLocation == null)
            {
                return; //_locationText.Text = "Unable to determine your location. Try again in a short while.";
            }
            else
            {
               
                Address newAddress = ReverseGeocodeCurrentLocation();
                if(newAddress.PostalCode != mCurrentAddress.PostalCode)
                {
                    library.SetCurrentLocation(location.Latitude, location.Longitude);
                    mCurrentAddress = newAddress;
                    ISharedPreferencesEditor editor = sharedPreferences.Edit();
                    editor.PutString("location_zipcode",newAddress.PostalCode);
                    editor.PutString("location_city", newAddress.Locality);
                    editor.Apply();
                }                      
            }
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
            _locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);

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
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        */
        /*
        public class UpdateWeatherTask : TimerTask
        {

            UpdateService mService;
            SQLLibrary library;
            ISharedPreferencesEditor editor;
            //double TO_MILES = 0.000621371;
            public UpdateWeatherTask(UpdateService service)
            {
                mService = service;
                library = SQLLibrary.getInstance();
              
            }
            public override void Run()
            {

                editor = mService.GetSharedPreferences().Edit();
                WeatherObject w = library.GetWeather(mService.GetLocationAddress().PostalCode);
                if (w != null)
                {

                 //   mSunrise = w.sys.sunrise;
                 //   mSunset = w.sys.sunset;
                 //   mWeatherId = w.weather[0].id;
                 //   mTemperature = w.main.temp;
                    editor.PutInt("weatherId", w.weather[0].id);
                    editor.PutInt("temperature", w.main.temp);
                    editor.PutLong("sunrise", w.sys.sunrise);
                    editor.PutLong("sunset", w.sys.sunset);
                    editor.Apply();
                }            
             
            }
            
        }
        */

        public class UpdateTimerTask : TimerTask
        {
            UpdateService mService;
            SQLLibrary library;
            double TO_MILES = 0.000621371;
            public UpdateTimerTask(UpdateService service)
            {
                mService = service;
                library = SQLLibrary.getInstance();
            }
            public override void Run()
            {
                // Call sql
                List<Restaurant> list =  library.GetRestaurants();
                SQLLibrary.CurrentLocation currentLocation = library.GetCurrentLocation();
                if (list == null) return;
                bool mFlag = false;
                foreach (Restaurant r in list)
                {
                    int id = r.RestaurantId;  //rest. id
                    if (!mService.doesExist(id))
                    {
                        string location = r.Name;
                        int time = r.CurrentWait;
                        ListItem item = new ListItem(mService.ApplicationContext, null);
                        item.Name = r.Name;
                        item.Index = id;
                        item.WaitTime = r.CurrentWait;
                        item.Address = r.Address;                      
                        string distance = CalculateDistance(currentLocation, r.Address);
                        item.Distance = distance;
                        item.setLables(r.Name, "Average Wait Time: " + r.CurrentWait + " mins", distance);
                        mService.addListItem(item);
                        mFlag = true;
                    }
                }

                if (mFlag)
                {
                    Intent intent = new Intent("com.csi4999.project16.ACTION_UPDATE");
                    mService.SendBroadcast(intent);
                }
            }
            private string CalculateDistance(SQLLibrary.CurrentLocation location, string address)
            {

                Location locA = new Location("Current");
                locA.Latitude = location.Latitude;
                locA.Longitude = location.Longitude;

                Location locB = GetLocationFromAddress(address);
                if(locB == null)
                {
                    return "";
                }
                double distance = locA.DistanceTo(locB);
                string city = ReverseGeocodeCurrentLocation(locB).Locality;
                return city + ", " + (distance * TO_MILES).ToString("0.##") + " mi";
                
            }
            private Location GetLocationFromAddress(string strAddress)
            {
                Location rLocation = null;
                Geocoder coder = new Geocoder(mService.ApplicationContext);
                IList<Address> address;
                try
                {
                    address = coder.GetFromLocationName(strAddress, 5);
                    Address location = address[0];
                    rLocation = new Location("Store");
                
                    rLocation.Latitude = location.Latitude;
                    rLocation.Longitude = location.Longitude;
                    return rLocation;

                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            private Address ReverseGeocodeCurrentLocation(Location loc)
            {
                try
                {
                    Geocoder geocoder = new Geocoder(mService);
                    IList<Address> addressList = geocoder.GetFromLocation(loc.Latitude, loc.Longitude, 10);

                    Address address = addressList.FirstOrDefault();
                    return address;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

    }
}