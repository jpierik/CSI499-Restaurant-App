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
    [Activity(Label = "Available Deals", Theme = "@style/Theme.AppCompat.Light", ParentActivity = typeof(DashboardActivity))]
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
            //mDealDescript = (TextView)FindViewById(Resource.Id.dealDescript);
            bool x = false;
            int y = 0;
            ResetImageCounters();
            foreach (Deal d in dealList)
            {
                if (x == false)
                {
                    DealItem item = new DealItem(ApplicationContext, null);

                    int sizeOfRestList = restList.Count();
                    int id = d.RestaurantId;
                    foreach (Restaurant r in restList)
                    {
                        if (id == r.RestaurantId)
                        {
                            item.restId = id;
                            item.restName = r.Name;
                            item.restWaitTime = r.CurrentWait;

                            item.SetName(r.Name);
                            break;
                        }
                    }
                    
                    y = d.category;
                    item.SetText(d.Title);
                    item.SetDescript(d.Descript);
                    item.SetImage(GetImageResource(y, d.Title));
                    dDealLayout.AddView(item);
                    x = true;
                }
                else
                {
                    DealItemRight item = new DealItemRight(ApplicationContext, null);

                    int sizeOfRestList = restList.Count();
                    int id = d.RestaurantId;
                    foreach (Restaurant r in restList)
                    {
                        if (id == r.RestaurantId)
                        {
                            item.restId = id;
                            item.restName = r.Name;
                            item.restWaitTime = r.CurrentWait;
                       
                            item.SetNameR(r.Name);
                            break;
                        }
                    }
                  
                    y = d.category;
                    item.SetTextR(d.Title);
                    item.SetDescriptR(d.Descript);
                    item.SetImageR(GetImageResource(y, d.Title));
                    dDealLayout.AddView(item);
                    x = false;
                }
            }

            
            // Create your application here


            

        }
        public override Boolean OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
        int mCat0 = 0;
        int mCat1 = 0;
        int mCat2Beer = 0;
        int mCat2Mixed = 0;
        int mCat3 = 0;

        private void ResetImageCounters()
        {
            mCat0 = 0;
            mCat1 = 0;
            mCat2Beer = 0;
            mCat2Mixed = 0;
            mCat3 = 0;
        }

        int[] mAppImages = { Resource.Drawable.chips_and_salsa, Resource.Drawable.mozz_sticks, Resource.Drawable.sliders, Resource.Drawable.nachos, Resource.Drawable.wings, Resource.Drawable.spinart };
        int[] mBeerImages = { Resource.Drawable.beer, Resource.Drawable.beer2, Resource.Drawable.beer3, Resource.Drawable.flight };
        int[] mDrinkImages = { Resource.Drawable.longisland, Resource.Drawable.margaritta, Resource.Drawable.mixeddrink, Resource.Drawable.moscowmuel };
        int[] mDessertImages = { Resource.Drawable.brownie, Resource.Drawable.cookie, Resource.Drawable.cookietower, Resource.Drawable.dessertcup };
        int[] mMealImages = { Resource.Drawable.crablegs, Resource.Drawable.pizza, Resource.Drawable.pasta, Resource.Drawable.steak, Resource.Drawable.sandwhich, Resource.Drawable.burger };

        private int GetImageResource(int category, string title)
        {
            switch (category)
            {
                case 0:
                    if (mCat0 == mMealImages.Length)
                        mCat0 = 0;
                    return mMealImages[mCat0++];
                case 2:
                    if (mCat1 == mAppImages.Length)
                        mCat1 = 0;
                    return mAppImages[mCat1++];
                case 1:
                    if (mCat2Mixed == mDrinkImages.Length)
                        mCat2Mixed = 0;
                    if (mCat2Beer == mBeerImages.Length)
                        mCat2Beer = 0;
                    if (title.ToLower().Contains("beer"))
                    {
                        return mBeerImages[mCat2Beer++];
                    }
                    else
                    {
                        return mDrinkImages[mCat2Mixed++];
                    }                   
                case 3:
                    if (mCat3 == mDessertImages.Length)
                        mCat3 = 0;
                    return mDessertImages[mCat3++];                   
                default:
                    if (mCat0 == mMealImages.Length)
                        mCat0 = 0;
                    return mMealImages[mCat0++];
                  
            }          
        }
    }    
}