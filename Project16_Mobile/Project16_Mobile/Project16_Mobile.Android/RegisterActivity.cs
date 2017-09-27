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
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {
        Button btnSignup;
        TextView lnkLogin;
        EditText txtName;
        EditText txtEmail;
        EditText txtPassword;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_register);
            txtName = (EditText)FindViewById(Resource.Id.inputName);
            txtEmail = (EditText)FindViewById(Resource.Id.inputEmail);
            txtPassword = (EditText)FindViewById(Resource.Id.inputPassword);
            btnSignup = (Button)FindViewById(Resource.Id.btnSignup);
            btnSignup.Click += delegate
            {
                
            };
            lnkLogin = (TextView)FindViewById(Resource.Id.lnkLogin);
            lnkLogin.Click += delegate
            {
                Finish();
            };

        }
        public void signup()
        {
            Console.WriteLine("Signup Started...");

            if (!validate(){
                onSignupFailed();
                return;
            }
            btnSignup.Enabled = false;
            ProgressDialog progressDialog = new ProgressDialog(Application.Context);
            progressDialog.SetMessage("Creating Account...");
            progressDialog.Show();

            string name = txtName.Text;
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
                    onSignupSuccess();
                }
                else
                {
                    onSignupFailed();
                }
                progressDialog.Dismiss();

            };

            h.PostDelayed(myAction, 3000);

        }
        public void onSignupSuccess()
        {
            btnSignup.Enabled = true;
            SetResult(Result.Ok, null);
            Finish();
        }
        public void onSignupFailed()
        {
            Toast.MakeText(Application.Context, "Registration failed", ToastLength.Long).Show();
            btnSignup.Enabled = true;
        }
        public bool validate()
        {

        }
    }
}