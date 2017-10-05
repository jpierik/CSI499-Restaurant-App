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
    [Activity(Label = "Project16_Mobile.Android", MainLauncher = false, Icon = "@drawable/icon")]
    public class SearchActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
           
            base.OnCreate(savedInstanceState);
            
            // Create your application here
            
            SetContentView (Resource.Layout.Search);

            TextView testText = FindViewById<TextView>(Resource.Id.testText);
            EditText searchBox = FindViewById<EditText>(Resource.Id.searchBox);
            ImageView searchIcon = FindViewById<ImageView>(Resource.Id.searchIcon);
            LinearLayout resultListLayout = FindViewById<LinearLayout>(Resource.Id.resultListLayout);
            //TextView b1 = new TextView(this);
            //resultListLayout.AddView(b1);


        }
    }
}