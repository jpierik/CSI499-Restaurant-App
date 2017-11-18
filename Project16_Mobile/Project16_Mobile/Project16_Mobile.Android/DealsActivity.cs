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
    [Activity(Label = "DealsActivity", Theme = "@style/Theme.AppCompat.Light", ParentActivity = typeof(ResturantActivity))]
    public class DealsActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Deals);

            SQLLibrary dLibrary = SQLLibrary.getInstance();
            var dealList = new List<Deal>(dLibrary.GetDeals());
            var restList = new List<Restaurant>(dLibrary.GetRestaurants());
            TextView dRestName = null, dDealText = null;
            LinearLayout dDealLayout = null;
            RelativeLayout dRelativeLayout = null;
            SetContentView(Resource.Layout.Deals);
           // dRelativeLayout = (RelativeLayout)FindViewById(Resource.Id.selectedDeal);
            dDealLayout = (LinearLayout)FindViewById(Resource.Id.dealListLayout);
            dRestName = (TextView)FindViewById(Resource.Id.restName);
            dDealText = (TextView)FindViewById(Resource.Id.dealText);
            mDealDescript = (TextView)FindViewById(Resource.Id.dealDescript);
            bool x = false;
            foreach (Deal d in dealList)
            {
                DealItem item = new DealItem(ApplicationContext, null);

                int sizeOfRestList = restList.Count();
                int id = d.RestaurantId;
                foreach (Restaurant r in restList)
                {
                    if (id == r.RestaurantId)
                    {                      
                        item.SetName(r.Name);
                        item.Name = r.Name;
                        item.Id = r.RestaurantId;
                    }
                }
                item.SetText(d.Title);
                item.SetDescript(d.Descript);
                dDealLayout.AddView(item);
            }

            
            // Create your application here
        }
    }
}