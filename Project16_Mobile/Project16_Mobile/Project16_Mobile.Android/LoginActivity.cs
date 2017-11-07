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
using Java.Lang;
using Xamarin;
using Android.Util;
using Android.Support.V7.App;
using Android;
using Android.Content.PM;

namespace Project16_Mobile.Droid
{



    [Activity(Label = "Login", Theme = "@style/Theme.AppCompat.Light")]
    public class LoginActivity : AppCompatActivity
    {

        Button btnLogin;
        TextView lnkSignup;
        EditText txtEmail;
        EditText txtPassword;
        public static int APP_PERMISSION = 2;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
          
            // Create your application here
            SetContentView(Resource.Layout.activity_login);
            txtEmail = (EditText)FindViewById(Resource.Id.txtUsername);
            txtPassword = (EditText)FindViewById(Resource.Id.txtPassword);
            btnLogin = (Button)FindViewById(Resource.Id.btnLogin);
            btnLogin.Click += delegate
            {
                login(this);
            };
            
            lnkSignup = (TextView)FindViewById(Resource.Id.lnkSignup);
            lnkSignup.Click += delegate
            {
                StartActivity(typeof(RegisterActivity));
            };
            AskForPermissions();

        }
       //[Android.Runtime.Register("onBackPressed", "()V", "GetOnBackPressedHandler")]
        public override void OnBackPressed()
        {
            base.OnBackPressed();
            MoveTaskToBack(true);
          
        }


        public void AskForPermissions()
        {
            if (Android.Support.V4.Content.ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessCoarseLocation) != (int)Permission.Granted ||
                Android.Support.V4.Content.ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessFineLocation) != (int)Permission.Granted ||
                Android.Support.V4.Content.ContextCompat.CheckSelfPermission(this, Manifest.Permission.Internet) != (int)Permission.Granted)
            {
                Android.Support.V4.App.ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.AccessFineLocation, Manifest.Permission.AccessCoarseLocation, Manifest.Permission.Internet }, APP_PERMISSION);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            switch (requestCode)
            {
                case 2:
                    {
                        if (grantResults[0] == Permission.Granted)
                        {

                        }
                    }
                    break;
            }
        }

        public void login(Context context)
        {
            Console.WriteLine("Login Begin...");
            if (!validate())
            {
                onLoginFailed();
                return;
            }
            btnLogin.Enabled = false;
            ProgressDialog progressDialog = new ProgressDialog(context);
            progressDialog.SetMessage("Authenticating...");
            progressDialog.Indeterminate = true;
            progressDialog.Show();

            string email = txtEmail.Text;
            string password = txtPassword.Text;
            SQLLibrary library = SQLLibrary.getInstance();
            Handler h = new Handler();
            Action myAction = () =>
            {
                // Testing Commit
                // your code that you want to delay here
                User user = library.Login(email, password);

                if (user != null)
                {
                    OnLoginSuccess(user);
                }
                else
                {
                    onLoginFailed();
                }
                progressDialog.Dismiss();

            };

            h.Post(myAction);


        }
        public void OnLoginSuccess(User user)
        {
            ISharedPreferences sharedPreferences = GetSharedPreferences("mypref", FileCreationMode.Private);
            ISharedPreferencesEditor editor = sharedPreferences.Edit();
            editor.PutString("username", user.email);
            editor.PutString("password", user.pwd);
            editor.PutInt("user_id", user.UserId);
            editor.PutString("fullname", user.FullName);
            editor.Apply();
            btnLogin.Enabled = true;
            StartActivity(typeof(DashboardActivity));
            Finish();
        }
        public void onLoginFailed()
        {
            Toast.MakeText(Application.Context, "Login Failed", ToastLength.Long).Show();
            btnLogin.Enabled = true;
        }
        public bool validate()
        {
            bool valid = true;
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            if(string.IsNullOrEmpty(email) || !Patterns.EmailAddress.Matcher(email).Matches())
            {
                txtEmail.Error = "Enter a valid email address";
                valid = false;
            }
            else
            {
                txtEmail.Error = null;
            }
            if(string.IsNullOrEmpty(password) || password.Length < 4 || password.Length > 10)
            {
                txtPassword.Error = "Between 4 and 10 alphanumeric characters";
                valid = false;
            }
            else
            {
                txtPassword.Error = null;
            }
            return valid;
        }
    }
}