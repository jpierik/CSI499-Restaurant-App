﻿using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Json;

namespace Project16_Mobile.Droid
{
	[Activity (Label = "Project16_Mobile.Android", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
            SQLLibrary library = SQLLibrary.getInstance();
			button.Click += async delegate {
                JsonValue json = await library.TestConnection ();
                json.ToString();
            };
          
		}
	}
}


