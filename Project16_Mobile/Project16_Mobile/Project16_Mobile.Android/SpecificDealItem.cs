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
    public class SpecificDealItem : RelativeLayout
    {
        TextView mRestName = null, mDealText = null, mDealDescript = null;
        RelativeLayout mSelectedDeal = null;

        public string Name { get; set; }
        public int Index { get; set; }

        public SpecificDealItem(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize(context);
        }
        private void Initialize(Context context)
        {

            LayoutInflater.From(context).Inflate(Resource.Layout.specific_deal_item_layout, this, true);
            mDealText = (TextView)FindViewById(Resource.Id.dealTextSP);
            mSelectedDeal = (RelativeLayout)FindViewById(Resource.Id.selectedDealSP);
            mDealDescript = (TextView)FindViewById(Resource.Id.dealDescriptSP);
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
    }
}