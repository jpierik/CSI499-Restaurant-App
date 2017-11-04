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
    [Activity(Label = "InLine", Theme = "@style/SplashTheme", MainLauncher = true, Icon = "@drawable/icon")]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ISharedPreferences sharedPreferences = GetSharedPreferences("mypref", FileCreationMode.Private);
            string username = sharedPreferences.GetString("username", null);
            string password = sharedPreferences.GetString("password", null);
            Intent login = new Intent(this, typeof(LoginActivity));
            if (username == null || password == null)
            {
                StartActivity(login);                
            }
            else
            {
                SQLLibrary library = SQLLibrary.getInstance();
                bool output = library.Login(username, password);
                if (output)
                {
                    Intent search = new Intent(this, typeof(SearchActivity));
                    StartActivity(search);
                }
                else
                {
                    StartActivity(login);
                }                
            }
            Finish();
        }
    }
}