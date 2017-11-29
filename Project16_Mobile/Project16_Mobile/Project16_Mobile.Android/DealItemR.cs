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
        TextView mRestNameR = null, mDealTextR = null, mDealDescriptR = null;
        RelativeLayout mSelectedDealR = null;

        public string Name { get; set; }
        public int Index { get; set; }

        public DealItem(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize(context);
        }
        private void Initialize(Context context)
        {

            LayoutInflater.From(context).Inflate(Resource.Layout.deal_item_layout_right, this, true);
            mRestNameR = (TextView)FindViewById(Resource.Id.restNameR);
            mDealTextR = (TextView)FindViewById(Resource.Id.dealTextR);
            mSelectedDealR = (RelativeLayout)FindViewById(Resource.Id.selectedDealR);
            mDealDescriptR = (TextView)FindViewById(Resource.Id.dealDescriptR);
            Index = -1;
            mSelectedDeal.Click += delegate
            {
                Intent intent = new Intent(context, typeof(ResturantActivity)/* Insert Reseraunt Activity*/);
                intent.PutExtra(UpdateService.EXTRA_RNAME, Name);
                context.StartActivity(intent);
            };
        }
        public void SetName(string name)
        {
            mRestNameR.Text = name;
        }
        public void SetText(string text)
        {
            mDealTextR.Text = text;
        }
        public void SetDescript(string text)
        {
            mDealDescriptR.Text = text;
        }
    }
}