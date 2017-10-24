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
        TextView mLocationName = null, mWaitTime = null;
        RelativeLayout mSelection = null;

        int mIndex = -1;

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
            mSelection = (RelativeLayout)FindViewById(Resource.Id.selectedLocation);

            mSelection.Click += delegate
            {
                Intent intent = new Intent(context, typeof( ResturantActivity)/* Insert Reseraunt Activity*/);
                intent.PutExtra(Constants.EXTRA_INDEX, mIndex);
                context.StartActivity(intent);
            };


        }
       public void setIndex(int index)
       {
             mIndex = index;
       }
       public void setLocationAndWaitTime(string location, string time)
        {
            mLocationName.Text = location;
            mWaitTime.Text = time;
            //mDistance.Text = distance;
        }
    }
}