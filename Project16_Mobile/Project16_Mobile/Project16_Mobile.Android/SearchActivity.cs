using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

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
        UpdateBroadcastReceiver mReceiver;
        UpdateService mUpdateService;


        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            // Create your application here


            SetContentView(Resource.Layout.Search);
            mReceiver = new UpdateBroadcastReceiver();

            Intent updateService = new Intent(this, typeof(UpdateService));
            BindService(updateService, new ServiceConnection(this), Bind.AutoCreate);




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
        protected override void OnResume()
        {
            base.OnResume();
            RegisterReceiver(mReceiver, mUpdateIntentFilter());

        }


        protected override void OnPause()
        {
            UnregisterReceiver(mReceiver);
            base.OnPause();
        }
        public IntentFilter mUpdateIntentFilter()
        {
            IntentFilter intentFilter = new IntentFilter();
            intentFilter.AddAction("com.csi4999.project16.ACTION_UPDATE");
            return intentFilter;
        }


        public class ServiceConnection : Java.Lang.Object, IServiceConnection
        {
            SearchActivity searchActivity;
            public bool isConnected { get; private set; }
            public UpdateService.LocalBinder localBinder { get; private set; }

            public ServiceConnection(SearchActivity activity)
            {
                isConnected = false;
                localBinder = null;
                searchActivity = activity;
            }
            public void OnServiceConnected(ComponentName name, IBinder service)
            {
                if (name.ClassName.Equals(typeof(UpdateService).Name))
                {
                    localBinder = service as UpdateService.LocalBinder;

                    searchActivity.RegisterReceiver(searchActivity.mReceiver, searchActivity.mUpdateIntentFilter());
                    searchActivity.mUpdateService = localBinder.Service;
                    searchActivity.mUpdateService.startUpdateTimer();


                }
            }

            public void OnServiceDisconnected(ComponentName name)
            {
                if (name.ClassName.Equals(typeof(UpdateService).Name))
                {
                    searchActivity.UnregisterReceiver(searchActivity.mReceiver);
                }
            }
        }


        [BroadcastReceiver(Enabled = true, Exported = false)]
        public class UpdateBroadcastReceiver : BroadcastReceiver
        {
            public override void OnReceive(Context context, Intent intent)
            {
                string action = intent.Action;

                switch (action)
                {
                    case "com.csi4999.project16.ACTION_UPDATE":
                        break;

                }

            }

        }
    }
}