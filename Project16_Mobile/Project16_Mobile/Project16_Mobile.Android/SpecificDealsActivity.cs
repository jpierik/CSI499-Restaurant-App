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
    [Activity(Label = "SpecificDealsActivity", Theme = "@style/Theme.AppCompat.Light", ParentActivity = typeof(ResturantActivity))]
    public class SpecificDealsActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SpecificDeal);

            int dID = 0;
            string dName = null;

            Intent intent = this.Intent;
            dID = intent.GetIntExtra(UpdateService.EXTRA_DEALS_ID, -1);
            dName = intent.GetStringExtra(UpdateService.EXTRA_RESTAURANT);

            this.Title = dName;

            SQLLibrary dLibrary = SQLLibrary.getInstance();
            var dealList = new List<Deal>(dLibrary.GetDeals(dID)); //We need to pass in the restaurant ID fro the restaurant activity to use this correctly
            TextView dDealRestNameSP = null, dDealTextSP = null;
            LinearLayout dDealLayout = null;
            RelativeLayout dRelativeLayout = null;
            SetContentView(Resource.Layout.Deals);
            dRelativeLayout = (RelativeLayout)FindViewById(Resource.Id.selectedDeal);
            dDealLayout = (LinearLayout)FindViewById(Resource.Id.dealListLayoutSP);
            dDealRestNameSP = (TextView)FindViewById(Resource.Id.dealRestNameSP);
            dDealTextSP = (TextView)FindViewById(Resource.Id.dealTextSP);
            bool x = false;
            int y = 0;

            foreach (Deal d in dealList)
            {
                if (x == false)
                {
                    SpecificDealItem item = new SpecificDealItem(ApplicationContext, null);

                    y = d.DealID;
                    int id = d.RestaurantId;
                    item.SetTextSP(d.Title);
                    item.SetDescriptSP(d.Descript);
                    item.SetImageSP(y);
                    dDealLayout.AddView(item);
                }

                else
                {
                    SpecificDealItemRight item = new SpecificDealItemRight(ApplicationContext, null);

                    y = d.DealId;
                    int id = d.RestaurantId;
                    item.SetTextSPR(d.Title);
                    item.SetDescriptSPR(d.Descript);
                    item.setImageSPR(y);
                    dDealLayout.AddView(item);
                }
            }
        }
    }
}