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

namespace Project16_Mobile.Droid
{
    [Activity(Label = "ResturantActivity")]
    public class ResturantActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);
            // Create your application here

            //Button backButton = FindViewById<Button>(Resource.Id.backButton);
        }
    }
}