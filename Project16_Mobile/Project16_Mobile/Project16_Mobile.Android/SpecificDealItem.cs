﻿using System;
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
        LinearLayout mSelectedDeal = null;
        ImageView mDealIcon = null;

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
            mSelectedDeal = (LinearLayout)FindViewById(Resource.Id.selectedDealSP);
            mDealDescript = (TextView)FindViewById(Resource.Id.dealDescriptSP);
            mDealIcon = (ImageView)FindViewById(Resource.Id.dealIconSP);
            Index = -1;
         
        }
        public void SetNameSP(string name)
        {
            mRestName.Text = name;
        }
        public void SetTextSP(string text)
        {
            mDealText.Text = text;
        }
        public void SetDescriptSP(string text)
        {
            mDealDescript.Text = text;
        }
        public void SetImageSP(int id)
        {
            mDealIcon.SetImageResource(id);
        }
    
    }
}