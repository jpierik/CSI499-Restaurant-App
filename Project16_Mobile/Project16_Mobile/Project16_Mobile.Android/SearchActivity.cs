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
    [Activity(Label = "InLine", Theme = "@style/Theme.AppCompat.Light")]
    public class SearchActivity : AppCompatActivity
    {

        public static string EXTRA_INDEX = "com.csi4800.project16.EXTRA_INDEX";
        public static string ACTION_UPDATE = "com.csi4800.project16.ACTION_UPDATE";

        UpdateBroadcastReceiver mReceiver;
        UpdateService mUpdateService;
        LinearLayout resultListLayout;
        EditText searchBox;
        ImageView searchIcon;

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            // Create your application here
            
            SetContentView(Resource.Layout.Search);
            mReceiver = new UpdateBroadcastReceiver();

            Intent updateService = new Intent(this, typeof(UpdateService));
            BindService(updateService, new ServiceConnection(this), Bind.AutoCreate);
            
            searchBox = FindViewById<EditText>(Resource.Id.searchBox);
            searchIcon = FindViewById<ImageView>(Resource.Id.searchIcon);
            resultListLayout = FindViewById<LinearLayout>(Resource.Id.resultListLayout);
            //LinearLayout.LayoutParams resultParams = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, 200);
            
            //Button b1 = new Button(this);

            searchIcon.Click += delegate
            {
                //resultListLayout.AddView(b1, resultParams);
                {
                    /*
                    for (int i = 1; i <= 10; i++)
                    {
                        ListItem listItem = new ListItem(ApplicationContext, null);
                        listItem.Index = i;
                        listItem.setLocationAndWaitTime("Location: " + i, (20 + i) + " min");
                        resultListLayout.AddView(listItem);
                    }
                    */
                }
            };
            //StartActivity(typeof(LoginActivity));
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
        
        public void removeItemsFromView()
        {
            resultListLayout.RemoveAllViews();
        }

        public void addItemToView(ListItem item)
        {
            resultListLayout.AddView(item);
        }

        public void addItemsToView(List<ListItem> list)
        {
            removeItemsFromView();
            foreach (ListItem item in list)
            {
                resultListLayout.AddView(item);
            }
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
                Console.WriteLine("Service Connected: " + name.ClassName);
               
                    localBinder = service as UpdateService.LocalBinder;

                    searchActivity.RegisterReceiver(searchActivity.mReceiver, searchActivity.mUpdateIntentFilter());
                    searchActivity.mUpdateService = localBinder.Service;
                    searchActivity.mUpdateService.startUpdateTimer();
                
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
                SearchActivity activity = (SearchActivity)context;
                switch (action)
                {
                    case "com.csi4999.project16.ACTION_UPDATE":
                        activity.addItemsToView(activity.mUpdateService.mResturantList);
                        break;

                }

            }

        }
    }
}