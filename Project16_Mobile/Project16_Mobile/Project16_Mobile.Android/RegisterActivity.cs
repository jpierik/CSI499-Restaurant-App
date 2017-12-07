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
using System.Threading.Tasks;

namespace Project16_Mobile.Droid
{
    [Activity(Label = "Register", Theme = "@style/CustomAppCompatTheme")]
    public class RegisterActivity : AppCompatActivity
    {
        Button btnSignup;
        TextView lnkLogin;
        EditText txtName;
        EditText txtEmail;
        EditText txtPassword;
        EditText txtConfirm;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_register);
            txtName = (EditText)FindViewById(Resource.Id.inputName);
            txtEmail = (EditText)FindViewById(Resource.Id.inputEmail);
            txtPassword = (EditText)FindViewById(Resource.Id.inputPassword);
            txtConfirm = (EditText)FindViewById(Resource.Id.confirmPassword);
          
            btnSignup = (Button)FindViewById(Resource.Id.btnSignup);
            btnSignup.Click += delegate
            {
                signup(this);
            };
            lnkLogin = (TextView)FindViewById(Resource.Id.lnkLogin);
            lnkLogin.Click += delegate
            {
                Finish();
            };

        }
        public async void RegisterUser(string name, string email, string password)
        {
            SQLLibrary library = SQLLibrary.getInstance();
            Task<bool> output = library.Register(name, email, password);
            bool value = await output;
            if (value)
            {
                onSignupSuccess();
            }
            else
            {
                onSignupFailed();
            }            
        }
        public void signup(Context context)
        {
            Console.WriteLine("Signup Started...");

            if (!validate()) {
                onSignupFailed();
                return;
            }
            btnSignup.Enabled = false;
            ProgressDialog progressDialog = new ProgressDialog(context);
            progressDialog.SetMessage("Creating Account...");
            progressDialog.Indeterminate = true;
            progressDialog.Show();

            string name = txtName.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            SQLLibrary library = SQLLibrary.getInstance();
            Handler h = new Handler();
            Action myAction = () =>
            {
                // your code that you want to delay here
                RegisterUser(name, email, password);             
               
            };

            h.Post(myAction);

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
            bool valid = true;

            string name = txtName.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            string confirm = txtConfirm.Text;

            if (string.IsNullOrEmpty(name) || name.Length < 3)
            {
                txtName.Error = "At least 3 characters";
                valid = false;
            }
            else
            {
                txtName.Error = null;
            }
            if (string.IsNullOrEmpty(email) || !Android.Util.Patterns.EmailAddress.Matcher(email).Matches()){
                txtEmail.Error = "Enter a vaild email address";
                valid = false;
            }
            else
            {
                txtEmail.Error = null;
            }
            if(string.IsNullOrEmpty(password) || password.Length < 4 || password.Length > 20)
            {
                txtPassword.Error = "Between 4 and 20 alphanumeric characters";
                valid = false;
            }
            else if (!password.Any(c => char.IsDigit(c)))
            {
                txtPassword.Error = "Must contain a number";
                valid = false;
            }
            else
            {
                txtPassword.Error = null;
            }
            if(string.IsNullOrEmpty(confirm) || !confirm.Equals(password))
            {
                txtConfirm.Error = "Passwords must match";
                valid = false;
            }
            else
            {
                txtConfirm.Error = null;
            }
            return valid;
        }
    }
}