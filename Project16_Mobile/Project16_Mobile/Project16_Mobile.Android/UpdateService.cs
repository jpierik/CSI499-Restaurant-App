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
using Java.Util;
using Android.Util;


namespace Project16_Mobile.Droid
{
    [Service]
    public class UpdateService : Service
    {
        public static string EXTRA_INDEX = "com.csi4999.project16.EXTRA_INDEX";
        public static string ACTION_UPDATE = "com.csi4999.project16.ACTION_UPDATE";
        public static string EXTRA_RESTAURANT = "com.csi4999.project16.EXTRA_RESTAURANT";
        public static string EXTRA_WAITTIME = "com.csi4999.project16.EXTRA_WAITTIME";
        public static string EXTRA_TABLES = "com.csi.project16.EXTRA_TABLES";
        public static string EXTRA_RNAME = "com.csi.project16.EXTRA_RNAME";

        public IBinder Binder { get; private set; }
        private Timer mUpateTimer;
        public List<ListItem> mResturantList;

        public override void OnCreate()
        {
            // This method is optional to implement
            base.OnCreate();
            mResturantList = new List<ListItem>();
           
        }

        public override IBinder OnBind(Intent intent)
        {
            // This method must always be implemented        
            this.Binder = new LocalBinder(this);
            Console.WriteLine("Service Bounded: " + Binder);
            return this.Binder;
        }

        public override bool OnUnbind(Intent intent)
        {
            // This method is optional to implement
       
            return base.OnUnbind(intent);
        }

        public override void OnDestroy()
        {
            // This method is optional to implement
           
            base.OnDestroy();
        }

        public class LocalBinder : Binder
        {
            public LocalBinder(UpdateService service)
            {
                this.Service = service;
            }
            public UpdateService Service { get; private set; }
        }

        public void startUpdateTimer()
        {
            if(mUpateTimer == null)
            {
                mUpateTimer = new Timer();
            }
            mUpateTimer.ScheduleAtFixedRate(new UpdateTimerTask(this),0,30000);
        }
        public void stopUpdateTimer()
        {
            if(mUpateTimer == null)
            {
                return;
            }
            mUpateTimer.Dispose();
        }
        public bool doesExist(int id)
        {
            if (mResturantList.Exists(e => e.Index == id)){
                return true;
            }
            return false;
        }
        public void addListItem(ListItem item)
        {
            mResturantList.Add(item);
        }
        public List<ListItem> getResturantList()
        {
            return mResturantList;
        }
        public class UpdateTimerTask : TimerTask
        {
            UpdateService mService;
            SQLLibrary library;
            public UpdateTimerTask(UpdateService service)
            {
                mService = service;
                library = SQLLibrary.getInstance();
            }
            public override void Run()
            {
                // Call sql
                List<Restaurant> list =  library.GetRestaurants();
                if (list == null) return;
                bool mFlag = false;
                foreach (Restaurant r in list)
                {
                    int id = r.RestaurantId;  //rest. id
                    if (!mService.doesExist(id))
                    {
                        string location = r.Name;
                        string time = "30:00";
                        ListItem item = new ListItem(mService.ApplicationContext, null);
                        item.Name = r.Name;
                        item.Index = id;
                        item.setNameAndWaitTime(location, time);
                        mService.addListItem(item);
                        mFlag = true;
                    }
                }

                if (mFlag)
                {
                    Intent intent = new Intent("com.csi4999.project16.ACTION_UPDATE");
                    mService.SendBroadcast(intent);
                }
            }
        }

    }
}