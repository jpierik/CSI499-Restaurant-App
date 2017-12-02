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
    public class SpecificDealItemRight : RelativeLayout
    {
        TextView mRestName = null, mDealText = null, mDealDescript = null;
        LinearLayout mSelectedDeal = null;
        ImageView mDealIcon = null;

        public string Name { get; set; }
        public int Index { get; set; }

        public SpecificDealItemRight(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize(context);
        }
        private void Initialize(Context context)
        {

            LayoutInflater.From(context).Inflate(Resource.Layout.specific_deal_item_layout_right, this, true);
            mDealText = (TextView)FindViewById(Resource.Id.dealDescriptSPR);
            mSelectedDeal = (LinearLayout)FindViewById(Resource.Id.selectedDealSPR);
            mDealDescript = (TextView)FindViewById(Resource.Id.dealDescriptSPR);
            mDealIcon = (ImageView)FindViewById(Resource.Id.dealIconSPR);
            Index = -1;
            mSelectedDeal.Click += delegate
            {
                Intent intent = new Intent(context, typeof(ResturantActivity)/* Insert Reseraunt Activity*/);
                intent.PutExtra(UpdateService.EXTRA_RNAME, Name);
                context.StartActivity(intent);
            };
        }
        public void SetNameSPR(string name)
        {
            mRestName.Text = name;
        }
        public void SetTextSPR(string text)
        {
            mDealText.Text = text;
        }
        public void SetDescriptSPR(string text)
        {
            mDealDescript.Text = text;
        }
        public void SetImageSPR(int id)
        {
            mDealIcon.SetImageResource(id);
        }

    }
}