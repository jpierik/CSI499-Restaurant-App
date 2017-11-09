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

namespace Project16_Mobile.Droid
{
    [Activity(Label = "Restaurant", Theme = "@style/Theme.AppCompat.Light", ParentActivity =typeof(SearchActivity))]
    
    public class ResturantActivity : AppCompatActivity
    {
        SQLLibrary library;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Resturant);

            EditText partySize = FindViewById<EditText>(Resource.Id.partySize);
            Button backButton = FindViewById<Button>(Resource.Id.backButton);
            Button inLine = FindViewById<Button>(Resource.Id.inLine);
            Button dealsButton = FindViewById<Button>(Resource.Id.dealsButton);
            Intent searchIntent = this.Intent;
            var dealsActivity = new Intent(this, typeof(DealsActivity));
            string restaurantExtra = searchIntent.GetStringExtra(UpdateService.EXTRA_RNAME);
            int id = searchIntent.GetIntExtra(UpdateService.EXTRA_RID, -1);
            int time = searchIntent.GetIntExtra(UpdateService.EXTRA_WAITTIME, -1); 

            backButton.Click += delegate
            {
               Finish();
            };

            inLine.Click += delegate
            {
                int sizeOfParty = int.Parse(partySize.Text);
                library = SQLLibrary.getInstance();
                User user = library.GetUser();
                InsertWaitingParty(id, sizeOfParty, user.FullName, user.UserId);            
             
            };

            dealsButton.Click += delegate {
                dealsActivity.PutExtra(UpdateService.EXTRA_RESTAURANT, restaurantExtra); //Need to add the restaurant name to this1
                StartActivity(dealsActivity);
            };

           
            TextView textView = FindViewById<TextView>(Resource.Id.resturantName);
            textView.Text = "" + restaurantExtra;
            TextView waitTime = FindViewById<TextView>(Resource.Id.waitTime);
            waitTime.Text = "Average Wait Time: " + time + " mins";
            // Create your application here

            //Button backButton = FindViewById<Button>(Resource.Id.backButton);

            Android.Support.V7.App.AlertDialog.Builder builder = new Android.Support.V7.App.AlertDialog.Builder(this);
            builder.SetTitle("");
            LayoutInflater inflater = this.LayoutInflater;
            View view = inflater.Inflate(Resource.Layout.PopupDeal, null);
            builder.SetView(view);
            dealsActivity.PutExtra(UpdateService.EXTRA_RESTAURANT, restaurantExtra); //Need to add the restaurant name to this
            builder.SetPositiveButton("View More", (s, e) => { StartActivity(dealsActivity); });
            builder.SetNegativeButton("Exit", (s, e) => { });
            builder.Show();
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
    }
   
}