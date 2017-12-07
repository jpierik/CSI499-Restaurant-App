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
    public class InlineItem : LinearLayout
    {
        TextView mLocationName = null, mNumOfGuests = null, mDate;
        ImageView checkIn = null, checkOut = null;
        SQLLibrary library;

        public string Name { get; set; }
        public int PartyId { get; set; }
        public string Address { get; set; }
        public int NumOfGuests { get; set; }
        public DateTime Date { get; set; }
        double TO_MILES = 0.000621371;
        Context mContext;

        public InlineItem(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize(context);
        }

        private void Initialize(Context context)
        {
            mContext = context;
            LayoutInflater.From(context).Inflate(Resource.Layout.inline_layout, this, true);

            library = SQLLibrary.getInstance();

            mLocationName = (TextView)FindViewById(Resource.Id.txtInlineName);
            mNumOfGuests = (TextView)FindViewById(Resource.Id.txtInlineNumOfGuest);
            mDate = (TextView)FindViewById(Resource.Id.txtInlineDate);
            checkIn = (ImageView)FindViewById(Resource.Id.btnCheckIn);

            checkIn.Click += delegate
            {
                if (CalculateDistance(library.GetCurrentLocation(), Address) < 5.0)                
                    UpdateWaitingParty();                
                else
                    Toast.MakeText(context, "You must be within 1 mile to check in!", ToastLength.Long).Show();
            };
            checkOut = (ImageView)FindViewById(Resource.Id.btnCheckOut);
            checkOut.Click += delegate
            {
                RemoveWaitingParty();                
            };


        }
        public void SetLabels(string newName, string newGuests, string date)
        {
            mLocationName.Text = newName;
            mNumOfGuests.Text = newGuests;
            mDate.Text = date;
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
            Geocoder coder = new Geocoder(mContext);
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
            Task<bool> output = library.UpdateWaitingParty(PartyId);
            bool value = await output;
            if (value)
            {
                Toast.MakeText(mContext, "You are now Checked In!", ToastLength.Long).Show();
            }
            else
            {
                Toast.MakeText(mContext, "Please see host/hostess to get checked in", ToastLength.Long).Show();
            }
        }
        public async void RemoveWaitingParty()
        {
            Task<bool> output = library.RemoveWaitingParty(PartyId);
            bool value = await output;
            if (value)
            {
                Toast.MakeText(mContext, "You are now Checked Out!", ToastLength.Long).Show();
                ((InlineActivity)mContext).RemoveItemFromView(PartyId);
            }
            else
            {
                Toast.MakeText(mContext, "Error: Please try again later", ToastLength.Long).Show();
            }
        }
    }
}