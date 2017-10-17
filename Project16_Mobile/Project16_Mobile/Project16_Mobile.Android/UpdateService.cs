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

using Android.Util;


namespace Project16_Mobile.Droid
{
    [Service]
    public class UpdateService : Service
    {
        public IBinder Binder { get; private set; }

        public override void OnCreate()
        {
            // This method is optional to implement
            base.OnCreate();       
           
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

        }
        public void stopUpdateTimer()
        {

        }

    }
}