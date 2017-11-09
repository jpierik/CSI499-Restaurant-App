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
    public class DealItem : RelativeLayout
    {
        TextView mRestName = null, mDealText = null;
        RelativeLayout mSelectedDeal = null;

        public string Name { get; set; }
        public int Index { get; set; }

        public DealItem(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize(context);
        }
        private void Initialize(Context context)
        {

            LayoutInflater.From(context).Inflate(Resource.Layout.deal_item_layout, this, true);
            mRestName = (TextView)FindViewById(Resource.Id.restName);
            mDealText = (TextView)FindViewById(Resource.Id.dealText);
            mSelectedDeal = (RelativeLayout)FindViewById(Resource.Id.selectedDeal);
            Index = -1;
            mSelectedDeal.Click += delegate
            {
                Intent intent = new Intent(context, typeof(ResturantActivity)/* Insert Reseraunt Activity*/);
                intent.PutExtra(UpdateService.EXTRA_RNAME, Name);
                context.StartActivity(intent);
            };


        }


        public void setNameAndWaitTime(string name, string time)
        {

            mRestName.Text = name;
            mDealText.Text = time;
        }
    }
}