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
using Android.Support.V7.App;

namespace Project16_Mobile.Droid
{
    [Activity(Label = "InlineActivity", Theme = "@style/Theme.AppCompat.Light")]
    public class InlineActivity : AppCompatActivity
    {
        SQLLibrary library;
        LinearLayout inlineView = null;
        List<InlineItem> itemList;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_inline);
            // Create your application here
           
            library = SQLLibrary.getInstance();
            User user = library.GetUser();
            List<WaitingParty> list = library.GetWaitingParties();
            inlineView = (LinearLayout)FindViewById(Resource.Id.inlineView);
            itemList = new List<InlineItem>();
            foreach (WaitingParty wp in list)
            {
                if (wp.MobileUserId == user.UserId)
                {
                    Restaurant restaurant = library.GetRestaurant(wp.RestaurantID);
                    if (restaurant != null)
                    {
                        InlineItem item = new InlineItem(this, null);
                        item.Name = restaurant.Name;
                        item.NumOfGuests = wp.NoOfGuests;
                        item.Date = wp.AddTime;
                        item.PartyId = wp.PartyId;
                        item.Address = restaurant.Address;
                        item.SetLabels(restaurant.Name, "Party Size: " + wp.NoOfGuests, wp.AddTime.ToLongDateString() + " - " + wp.AddTime.ToShortTimeString());
                        itemList.Add(item);
                        inlineView.AddView(item);

                    }
                }
                   
            }
        }
        public void RemoveItemFromView(int pID)
        {
            for(int i = 0; i < itemList.Count; i++)
            {
                if(itemList[i].PartyId == pID)
                {
                    inlineView.RemoveView(itemList[i]);
                }
            }
        }
    }
}