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
            // Create your application here
        }
    }
}