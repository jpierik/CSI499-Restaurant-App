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
    public class DealItemRight : RelativeLayout
    {
        TextView mRestName = null, mDealText = null, mDealDescript = null;
        LinearLayout mSelectedDeal = null;
        ImageView mDealIcon = null;

        public string Name { get; set; }

        public int restId { get; set; }
        public int restWaitTime { get; set; }
        public string restName { get; set; }

        public int Index { get; set; }

        public DealItemRight(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize(context);
        }
        private void Initialize(Context context)
        {

            LayoutInflater.From(context).Inflate(Resource.Layout.deal_item_layout_right, this, true);
            mRestName = (TextView)FindViewById(Resource.Id.restNameR);
            mDealText = (TextView)FindViewById(Resource.Id.dealTextR);
            mSelectedDeal = (LinearLayout)FindViewById(Resource.Id.selectedDealR);
            mDealDescript = (TextView)FindViewById(Resource.Id.dealDescriptR);
            mDealIcon = (ImageView)FindViewById(Resource.Id.dealIconR);
            Index = -1;
            mSelectedDeal.Click += delegate
            {
                Intent intent = new Intent(context, typeof(ResturantActivity)/* Insert Reseraunt Activity*/);
                intent.PutExtra(UpdateService.EXTRA_RNAME, restName);
                intent.PutExtra(UpdateService.EXTRA_RID, restId);
                intent.PutExtra(UpdateService.EXTRA_WAITTIME, restWaitTime);
                context.StartActivity(intent);
            };
        }
        public void SetNameR(string name)
        {
            mRestName.Text = name;
        }
        public void SetTextR(string text)
        {
            mDealText.Text = text;
        }
        public void SetDescriptR(string text)
        {
            mDealDescript.Text = text;
        }
      
        public void SetImageR(int id)
        {
            mDealIcon.SetImageResource(id);
        }
    }
}