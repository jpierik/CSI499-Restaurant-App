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
    public class DealItemRight : RelativeLayout
    {
        TextView mRestName = null, mDealText = null, mDealDescript = null;
        LinearLayout mSelectedDeal = null;
        ImageView mDealIcon = null;

        public string Name { get; set; }
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
                intent.PutExtra(UpdateService.EXTRA_RNAME, Name);
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
        public void SetImageR(int x)
        {
            int a = 0;
            Random rand = new Random(123456789);
            if (x == 0)
            {
                a = rand.Next(1, 6);
                switch (a)
                {
                    case 1:
                        mDealIcon.SetImageResource(Resource.Drawable.app1);
                        break;
                    case 2:
                        mDealIcon.SetImageResource(Resource.Drawable.app2);
                        break;
                    case 3:
                        mDealIcon.SetImageResource(Resource.Drawable.app3);
                        break;
                    case 4:
                        mDealIcon.SetImageResource(Resource.Drawable.app4);
                        break;
                    case 5:
                        mDealIcon.SetImageResource(Resource.Drawable.app5);
                        break;
                    case 6:
                        mDealIcon.SetImageResource(Resource.Drawable.app6);
                        break;
                    default:
                        mDealIcon.SetImageResource(Resource.Drawable.app1);
                        break;
                }
            }
            if (x == 1)
            {
                a = rand.Next(1, 9);
                switch (a)
                {
                    case 1:
                        mDealIcon.SetImageResource(Resource.Drawable.drink1);
                        break;
                    case 2:
                        mDealIcon.SetImageResource(Resource.Drawable.drink2);
                        break;
                    case 3:
                        mDealIcon.SetImageResource(Resource.Drawable.drink3);
                        break;
                    case 4:
                        mDealIcon.SetImageResource(Resource.Drawable.drink4);
                        break;
                    case 5:
                        mDealIcon.SetImageResource(Resource.Drawable.drink5);
                        break;
                    case 6:
                        mDealIcon.SetImageResource(Resource.Drawable.drink6);
                        break;
                    case 7:
                        mDealIcon.SetImageResource(Resource.Drawable.drink7);
                        break;
                    case 8:
                        mDealIcon.SetImageResource(Resource.Drawable.drink8);
                        break;
                    case 9:
                        mDealIcon.SetImageResource(Resource.Drawable.drink9);
                        break;
                    default:
                        mDealIcon.SetImageResource(Resource.Drawable.drink1);
                        break;
                }

            }
            if (x == 2)
            {
                a = rand.Next(1, 7);
                switch (a)
                {
                    case 1:
                        mDealIcon.SetImageResource(Resource.Drawable.meal1);
                        break;
                    case 2:
                        mDealIcon.SetImageResource(Resource.Drawable.meal2);
                        break;
                    case 3:
                        mDealIcon.SetImageResource(Resource.Drawable.meal3);
                        break;
                    case 4:
                        mDealIcon.SetImageResource(Resource.Drawable.meal4);
                        break;
                    case 5:
                        mDealIcon.SetImageResource(Resource.Drawable.meal5);
                        break;
                    case 6:
                        mDealIcon.SetImageResource(Resource.Drawable.meal6);
                        break;
                    case 7:
                        mDealIcon.SetImageResource(Resource.Drawable.meal7);
                        break;
                    default:
                        mDealIcon.SetImageResource(Resource.Drawable.meal1);
                        break;
                }
            }
            if (x == 3)
            {
                switch (a)
                {
                    case 1:
                        mDealIcon.SetImageResource(Resource.Drawable.dessert1);
                        break;
                    case 2:
                        mDealIcon.SetImageResource(Resource.Drawable.dessert2);
                        break;
                    case 3:
                        mDealIcon.SetImageResource(Resource.Drawable.dessert3);
                        break;
                    case 4:
                        mDealIcon.SetImageResource(Resource.Drawable.dessert4);
                        break;
                    default:
                        mDealIcon.SetImageResource(Resource.Drawable.dessert1);
                        break;
                }
            }
        }
    }
}