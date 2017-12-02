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
        TextView mRestName = null, mDealText = null, mDealDescript = null;
        LinearLayout mSelectedDeal = null;
        ImageView mDealIcon = null;

        public int restId { get; set; }
        public int restWaitTime { get; set; }
        public string restName { get; set; }

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
            mDealIcon = (ImageView)FindViewById(Resource.Id.dealIcon);
            mSelectedDeal = (LinearLayout)FindViewById(Resource.Id.selectedDeal);
            mDealDescript = (TextView)FindViewById(Resource.Id.dealDescript);
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
        public void SetName(string name)
        {
            mRestName.Text = name;
        }
        public void SetText(string text)
        {
            mDealText.Text = text;
        }
        public void SetDescript(string text)
        {
            mDealDescript.Text = text;
        }

        public void SetImage(int id)
        {
            mDealIcon.SetImageResource(id);
        }
    }
}