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
    [Activity(Label = "ResturantActivity", Theme = "@style/Theme.AppCompat.Light")]
    public class ResturantActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Resturant);

            EditText partySize = FindViewById<EditText>(Resource.Id.partySize);
            Button backButton = FindViewById<Button>(Resource.Id.backButton);
            Button inLine = FindViewById<Button>(Resource.Id.inLine);

            backButton.Click += delegate
            {
               Finish();
            };

            inLine.Click += delegate
            {
                int sizeOfParty = int.Parse(partySize.Text);
            };

            Intent intent = this.Intent;
            int index =  intent.GetIntExtra(Constants.EXTRA_INDEX, -1);

            TextView textView = FindViewById<TextView>(Resource.Id.resturantName);
            textView.Text = "" + index;
            TextView waitTime = FindViewById<TextView>(Resource.Id.waitTime);
            waitTime.Text = "" + (20 + index);
            // Create your application here

            //Button backButton = FindViewById<Button>(Resource.Id.backButton);
        }
    }
}