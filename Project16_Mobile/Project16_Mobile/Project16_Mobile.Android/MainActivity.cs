using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Json;

namespace Project16_Mobile.Droid
{
	[Activity (Label = "Project16_Mobile.Android", MainLauncher = true, Icon = "@drawable/logo")]
	public class MainActivity : Activity
	{
		//int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

            /*
                       Intent intent = new Intent(this, Settings.class);
                       startActivity(intent);
            */
            
            

			// Get our button from the layout resource,
			// and attach an event to it
            /*
			Button btnLogin = FindViewById<Button> (Resource.Id.btnLogin);
            Button btnRegister = FindViewById<Button>(Resource.Id.btnRegister);
            EditText txtUsername = FindViewById<EditText>(Resource.Id.txtUsername);
            EditText txtPassword = FindViewById<EditText>(Resource.Id.txtPassword);
            ProgressBar prgStatus = FindViewById<ProgressBar>(Resource.Id.prgStatus);
            SQLLibrary library = SQLLibrary.getInstance();
            btnLogin.Click += delegate {
                string output = library.TestConnection(txtUsername.Text,txtPassword.Text,"LOGIN");
                if (output.Contains("Success")){
                    StartActivity(typeof(SearchActivity));
                    //prgStatus.SetBackgroundColor(Android.Graphics.Color.Green);
                }
                else
                {
                    prgStatus.SetBackgroundColor(Android.Graphics.Color.Red);
                }
                
            };
            btnRegister.Click += delegate {
                string output = library.TestConnection(txtUsername.Text, txtPassword.Text, "REGISTER");
                if (output.Contains("Success")){
                    prgStatus.SetBackgroundColor(Android.Graphics.Color.Green);
                }
                else
                {
                    prgStatus.SetBackgroundColor(Android.Graphics.Color.Red);
                }
            };
            */

        }
	}
}


