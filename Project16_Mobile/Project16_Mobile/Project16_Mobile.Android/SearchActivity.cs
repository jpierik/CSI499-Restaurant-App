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
    [Activity(Label = "Project16_Mobile.Android", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat.Light")]
    public class SearchActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
           
            base.OnCreate(savedInstanceState);
            
            // Create your application here
            
            SetContentView (Resource.Layout.Search);

<<<<<<< HEAD
=======
            StartActivity(typeof(LoginActivity));

            TextView testText = FindViewById<TextView>(Resource.Id.testText);
>>>>>>> master
            EditText searchBox = FindViewById<EditText>(Resource.Id.searchBox);
            ImageView searchIcon = FindViewById<ImageView>(Resource.Id.searchIcon);
            LinearLayout resultListLayout = FindViewById<LinearLayout>(Resource.Id.resultListLayout);
            LinearLayout.LayoutParams resultParams = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, 200);



            //Button b1 = new Button(this);

            searchIcon.Click += delegate
            {
                //resultListLayout.AddView(b1, resultParams);
                {
                    for (int i = 1; i <= 10; i++)
                    {
                        resultListLayout.AddView(new Button(this), i, resultParams);
                        resultListLayout.GetChildAt(i).Id = i;
                        var childView = resultListLayout.GetChildAt(i);
                        if (childView is Button){
                            //childView.Text = "Resturant" + i;
                        }
                        resultListLayout.GetChildAt(i).Click += (sender, ea)=>
                        {
                            var temp = (Button)sender;
                            Intent intent = new Intent(this, typeof(ResturantActivity));
                            intent.PutExtra(MyClass.PASS_ID, temp.Id);
                            StartActivity(intent);
                        };
                    }
                }
            };
            searchBox.Click += delegate
            {
                searchBox.Text = "";
            };
        }  
    }
}