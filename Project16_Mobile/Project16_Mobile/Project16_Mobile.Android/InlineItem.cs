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
    public class InlineItem : LinearLayout
    {
        TextView mLocationName = null, mInfo = null;
        Button mSelection = null;

        public string Name { get; set; }
        public string Info { get; set; }

        public InlineItem(Context context, IAttributeSet attrs) :
            base(context, attrs){
            Initialize(context);
        }

        private void Initialize(Context context)
        {
            LayoutInflater.From(context).Inflate(Resource.Layout.inline_layout, this, true);
            mSelection = (Button)FindViewById(Resource.Id.btnInlineView);
            mLocationName = (TextView)FindViewById(Resource.Id.txtInlineName);
            mInfo = (TextView)FindViewById(Resource.Id.txtInlineOther);

            mSelection.Click += delegate
            {
                Intent intent = new Intent(context, typeof(InlineActivity));
                context.StartActivity(intent);
            };
        }
    }
}