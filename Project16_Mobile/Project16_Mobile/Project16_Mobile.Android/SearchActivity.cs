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
    [Activity(Label = "Project16_Mobile.Android", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat.Light")]
    public class SearchActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
           
            base.OnCreate(savedInstanceState);
            
            // Create your application here
           
            
            SetContentView (Resource.Layout.Search);


          

            EditText searchBox = FindViewById<EditText>(Resource.Id.searchBox);
            ImageView searchIcon = FindViewById<ImageView>(Resource.Id.searchIcon);
            LinearLayout resultListLayout = FindViewById<LinearLayout>(Resource.Id.resultListLayout);
            //LinearLayout.LayoutParams resultParams = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, 200);

    

            //Button b1 = new Button(this);

            searchIcon.Click += delegate
            {
                //resultListLayout.AddView(b1, resultParams);
                {
                    for (int i = 1; i <= 10; i++)
                    {
                        ListItem listItem = new ListItem(ApplicationContext, null);
                        listItem.setIndex(i);
                        listItem.setLocationAndWaitTime("Location: " + i, (20 + i) + " min");
                        resultListLayout.AddView(listItem);
                    }
                }
            };
           
            StartActivity(typeof(LoginActivity));
            
        }
    }
}