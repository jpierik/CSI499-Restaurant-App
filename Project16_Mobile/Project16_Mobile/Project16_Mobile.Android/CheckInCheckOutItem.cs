using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Locations;
using System.Threading.Tasks;

namespace Project16_Mobile.Droid
{
    public class CheckInCheckOutItem : LinearLayout
    {
        Context applicationContext;
        Context builderContext;
        SQLLibrary library;
        ImageView checkIn = null, checkOut = null;
        public int ID { get; set; }
        public string Address { get; set; }
        double TO_MILES = 0.000621371;

        public CheckInCheckOutItem(Context aContext, Context bContext, IAttributeSet attrs) :
            base(aContext, attrs)
        {
            Initialize(aContext, bContext);
        }       

        private void Initialize(Context aContext, Context bContext)           
        {
            LayoutInflater.From(bContext).Inflate(Resource.Layout.PopupInline, this, true);
            applicationContext = aContext;
            builderContext = bContext;
            library = SQLLibrary.getInstance();
            checkIn = (ImageView)FindViewById(Resource.Id.prevCheckIn);
            checkIn.Click += delegate
            {
                if (CalculateDistance(library.GetCurrentLocation(), Address) < 5.0)
                    UpdateWaitingParty();
                else
                    Toast.MakeText(applicationContext, "You must be within 1 mile to check in!", ToastLength.Long).Show();
            };
            checkOut = (ImageView)FindViewById(Resource.Id.prevCheckOut);
            checkOut.Click += delegate
            {
                RemoveWaitingParty();
            };
        }
        private double CalculateDistance(SQLLibrary.CurrentLocation location, string address)
        {

            Location locA = new Location("Current");
            locA.Latitude = location.Latitude;
            locA.Longitude = location.Longitude;

            Location locB = GetLocationFromAddress(address);
            if (locB == null)
            {
                return 0;
            }
            double distance = locA.DistanceTo(locB);
            return distance * TO_MILES;

        }
        private Location GetLocationFromAddress(string strAddress)
        {
            Location rLocation = null;
            Geocoder coder = new Geocoder(applicationContext);
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
        public async void UpdateWaitingParty()
        {
            Task<bool> output = library.UpdateWaitingParty(ID);
            bool value = await output;
            if (value)
            {
                Toast.MakeText(applicationContext, "You are now Checked In!", ToastLength.Long).Show();
            }
            else
            {
                Toast.MakeText(applicationContext, "Please see host/hostess to get checked in", ToastLength.Long).Show();
            }
        }
        public async void RemoveWaitingParty()
        {
            Task<bool> output = library.RemoveWaitingParty(ID);
            bool value = await output;
            if (value)
            {
                Toast.MakeText(applicationContext, "You are now Checked Out!", ToastLength.Long).Show();
                // ((InlineActivity)applicationContext).RemoveItemFromView(ID);
                ((DashboardActivity)applicationContext).DismissDialog();
            }
            else
            {
                Toast.MakeText(applicationContext, "Error: Please try again later", ToastLength.Long).Show();
            }
        }
    }
}