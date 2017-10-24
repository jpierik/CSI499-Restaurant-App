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
            mUpateTimer.ScheduleAtFixedRate(new UpdateTimerTask(this),0,10000);
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
            public UpdateTimerTask(UpdateService service)
            {
                mService = service;
            }
            public override void Run()
            {
                // Call sql

                int id = 0;  //rest. id
                if (!mService.doesExist(id))
                {
                    string location = "";
                    string time = "";
                    ListItem item = new ListItem(mService.ApplicationContext, null);
                    item.Index = id;
                    item.setLocationAndWaitTime(location, time);
                    mService.addListItem(item);
                }
                
                
                Intent intent = new Intent("com.csi4999.project16.ACTION_UPDATE");
                mService.SendBroadcast(intent);
            }
        }

    }
}