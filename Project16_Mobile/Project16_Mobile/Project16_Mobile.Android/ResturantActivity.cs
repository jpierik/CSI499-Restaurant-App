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
using System.Threading.Tasks;
using Android.Util;

namespace Project16_Mobile.Droid
{
    [Activity(Label = "Restaurant", Theme = "@style/Theme.AppCompat.Light", ParentActivity = typeof(SearchActivity))]
 
    public class ResturantActivity : AppCompatActivity
    {
        
        SQLLibrary library;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Resturant);

            EditText partySize = FindViewById<EditText>(Resource.Id.partySize);
           
            Button inLine = FindViewById<Button>(Resource.Id.inLine);
            Button dealsButton = FindViewById<Button>(Resource.Id.dealsButton);
          



            Intent searchIntent = this.Intent;
            var specificDealsActivity = new Intent(this, typeof(SpecificDealsActivity));
            
          
            int id = searchIntent.GetIntExtra(UpdateService.EXTRA_RID, -1);
            string restaurantExtra = searchIntent.GetStringExtra(UpdateService.EXTRA_RNAME);
            int time = searchIntent.GetIntExtra(UpdateService.EXTRA_WAITTIME, -1);
            string address = searchIntent.GetStringExtra(UpdateService.EXTRA_ADDRESS);
            string distance = searchIntent.GetStringExtra(UpdateService.EXTRA_DISTANCE);
            

            library = SQLLibrary.getInstance();

        

            inLine.Click += delegate
            {
                int sizeOfParty = int.Parse(partySize.Text);
                //library = SQLLibrary.getInstance();
                User user = library.GetUser();
                InsertWaitingParty(id, sizeOfParty, user.FullName, user.UserId);            
             
            };

            dealsButton.Click += delegate {
                specificDealsActivity.PutExtra(UpdateService.EXTRA_RESTAURANT, restaurantExtra); //Need to add the restaurant name to this1
                specificDealsActivity.PutExtra(UpdateService.EXTRA_DEALS_ID, restaurantExtra); //Need to add the restaurant name to this1
                StartActivity(specificDealsActivity);
            };

           
            TextView textView = FindViewById<TextView>(Resource.Id.resturantName);
            textView.Text = "" + restaurantExtra;
            textView.SetBackgroundResource(GetResourceImage());

            TextView addressView = FindViewById<TextView>(Resource.Id.txtRAddress);
            addressView.Text = address;

            TextView distanceView = FindViewById<TextView>(Resource.Id.txtRDistance);
            distanceView.Text = distance;

            TextView waitTime = FindViewById<TextView>(Resource.Id.waitText);
            waitTime.Text = "" + time + " mins";
            // Create your application here


            //Button backButton = FindViewById<Button>(Resource.Id.backButton);
            List<Deal> deals = library.GetDeals();
            foreach(Deal item in deals)
            {
                if(item.Priority && item.RestaurantId == id)
                {
                    Android.Support.V7.App.AlertDialog.Builder builder = new Android.Support.V7.App.AlertDialog.Builder(this);
                    builder.SetTitle(item.Title);
                    builder.SetMessage(item.Descript);

                    ImageView image = new ImageView(this);
                    image.SetMinimumWidth(200);
                    image.SetMinimumHeight(200);                    
                  
                    switch (item.category)
                    {
                        case 0:
                            image.SetBackgroundResource(Resource.Drawable.steak);
                            break;
                        case 1:
                            image.SetBackgroundResource(Resource.Drawable.beer);
                            break;
                        case 2:
                            image.SetBackgroundResource(Resource.Drawable.chips_and_salsa);
                            break;
                        case 3:
                            image.SetBackgroundResource(Resource.Drawable.dessertcup);
                            break;
                        default:
                            break;
                    }
                    builder.SetView(image);
                    specificDealsActivity.PutExtra(UpdateService.EXTRA_RESTAURANT, restaurantExtra); //Need to add the restaurant name to this
                    builder.SetPositiveButton("View More", (s, e) => { StartActivity(specificDealsActivity); });
                    builder.SetNegativeButton("Exit", (s, e) => { });
                    builder.Show();
                    return;
                }
            }


           
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


        protected override void OnResume()
        {
            base.OnResume();
        }
        public async void InsertWaitingParty(int id, int size, string name, int uId)
        {
            Task<bool> output = library.InsertWaitingParty(id, size, name, uId);
            bool value = await output;

            if (!value)
                Toast.MakeText(ApplicationContext, "Error: Please try again later", ToastLength.Long).Show();
            else
                Toast.MakeText(ApplicationContext, "Success: You are InLine!", ToastLength.Long).Show();
        }
        int[] drawableSelction = {Resource.Drawable.burger, Resource.Drawable.mozz_sticks, Resource.Drawable.wings, Resource.Drawable.sliders, Resource.Drawable.crablegs, Resource.Drawable.nachos,
                                   Resource.Drawable.pizza, Resource.Drawable.pasta, Resource.Drawable.salmon, Resource.Drawable.sandwhich};
        public int GetResourceImage()
        {
             return drawableSelction[library.GetNextRandom()];
        }
    }
   
}