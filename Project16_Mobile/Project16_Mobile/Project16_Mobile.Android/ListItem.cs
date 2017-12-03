using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Project16_Mobile.Droid
{
    public class ListItem : RelativeLayout
    {
        TextView mLocationName = null, mWaitTime = null, mDistance = null;
        RelativeLayout mSelection = null;

        public string Name { get; set; }
        public int Index { get; set; }
        public string Address { get; set; }
        public int WaitTime { get; set; }
        public string Distance { get; set; }

        public ListItem(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize(context);
        }
        private void Initialize(Context context)
        {
            
            LayoutInflater.From(context).Inflate(Resource.Layout.list_item_layout, this, true);
            mLocationName = (TextView)FindViewById(Resource.Id.lblName);
            mWaitTime = (TextView)FindViewById(Resource.Id.lblWaitTime);
            mDistance = (TextView)FindViewById(Resource.Id.lblDistance);
            mSelection = (RelativeLayout)FindViewById(Resource.Id.selectedLocation);
            Index = -1;
            mSelection.Click += delegate
            {
                Intent intent = new Intent(context, typeof(ResturantActivity)/* Insert Reseraunt Activity*/);
                TaskStackBuilder taskStackBuilder = TaskStackBuilder.Create(context);
                taskStackBuilder.AddNextIntentWithParentStack(intent);
                intent.PutExtra(UpdateService.EXTRA_RNAME, Name);
                intent.PutExtra(UpdateService.EXTRA_RID, Index);
                intent.PutExtra(UpdateService.EXTRA_WAITTIME, WaitTime);
                intent.PutExtra(UpdateService.EXTRA_ADDRESS, Address);
                intent.PutExtra(UpdateService.EXTRA_DISTANCE, Distance);
                context.StartActivity(intent);
            };


        }
       

        public void setLables(string name, string time, string distance)
        {
            
            mLocationName.Text = name;
            mWaitTime.Text = time;
            mDistance.Text = distance;
        }
    }
}