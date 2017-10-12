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

namespace Project16_Mobile.Droid
{



    [Activity(Label = "LoginActivity", Theme = "@style/Theme.AppCompat.Light")]
    public class LoginActivity : AppCompatActivity
    {

        Button btnLogin;
        TextView lnkSignup;
        EditText txtEmail;
        EditText txtPassword;

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

        }
       //[Android.Runtime.Register("onBackPressed", "()V", "GetOnBackPressedHandler")]
        public override void OnBackPressed()
        {
            base.OnBackPressed();
            MoveTaskToBack(true);
          
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
                // your code that you want to delay here
                string output = library.TestConnection(email, password, "LOGIN");
                if (output.Contains("Success"))
                {
                    onLoginSuccess();
                }
                else
                {
                    onLoginFailed();
                }
                progressDialog.Dismiss();

            };

            h.Post(myAction);


        }
        public void onLoginSuccess()
        {
            btnLogin.Enabled = true;
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