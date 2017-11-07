using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.PM;
using Android.Support.Design.Widget;

namespace Project16_Mobile.Droid
{
    [Activity(Label = "InLine", Theme = "@style/SplashTheme", MainLauncher = true, Icon = "@drawable/logo")]
    public class SplashActivity : Activity
    {

        bool mFlag = true;
     

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

         

            ISharedPreferences sharedPreferences = GetSharedPreferences("mypref", FileCreationMode.Private);
            string username = sharedPreferences.GetString("username", null);
            string password = sharedPreferences.GetString("password", null);
            int id = sharedPreferences.GetInt("user_id", -1);
            string fullname = sharedPreferences.GetString("fullname", null);

            Intent login = new Intent(this, typeof(LoginActivity));
            if (username == null || password == null || id == -1)
            {
                StartActivity(login);
            }
            else
            {

                SQLLibrary library = SQLLibrary.getInstance();
                User user = library.Login(username, password);
                if (user != null)
                {
                    Intent search = new Intent(this, typeof(DashboardActivity));
                    search.PutExtra("com.csi4999.inline.EXTRA_USER_ID", id);
                    search.PutExtra("com.csi4999.inline.EXTRA_USER_FULLNAME", fullname);                
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