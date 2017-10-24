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

        public int Index { get; set; }
        public string Address { get; set; }

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
            Index = -1;
            mSelection.Click += delegate
            {
                Intent intent = new Intent(context, typeof(ResturantActivity)/* Insert Reseraunt Activity*/);
                intent.PutExtra(UpdateService.EXTRA_INDEX, Index);
                context.StartActivity(intent);
            };


        }
       

        public void setNameAndWaitTime(string name, string time)
        {
            mLocationName.Text = name;
            mWaitTime.Text = time;
        }
    }
}