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
            TextView dRestName = null, dDealText = null;
            ScrollView dScrollView = null;
            RelativeLayout dRelativeLayout = null;
            SetContentView(Resource.Layout.Deals);
            dRelativeLayout = (RelativeLayout)FindViewById(Resource.Id.selectedDeal);
            dScrollView = (ScrollView)FindViewById(Resource.Id.dealsView);
            dRestName = (TextView)FindViewById(Resource.Id.restName);
            dDealText = (TextView)FindViewById(Resource.Id.dealText);

            foreach (Deal d in dealList)
            {
                int id = d.DealId;
                dRestName.Text = Name;  //passed in with the intent from the ResturantActivity class, don't know how to utilize it yet
                dDealText.Text = Deal.Descript; //Don't know how exactly to reference the values of Deal.cs
                dScrollView.AddView(dRelativeLayout);
            }

            
            // Create your application here
        }
    }
}