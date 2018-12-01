﻿using Android.App;
using Android.OS;
using Android.Widget;

namespace ListApp
{
	[Activity(Label = "PersonDetail")]
	public class PersonDetail : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.person_detail);

			var fldFirstname = FindViewById<TextView>(Resource.Id.detailItemFirstname);
			fldFirstname.Text = Intent.GetStringExtra("Firstname");
			var fldLastname = FindViewById<TextView>(Resource.Id.detailItemLastname);
			fldLastname.Text = Intent.GetStringExtra("Lastname");
		}
	}
}