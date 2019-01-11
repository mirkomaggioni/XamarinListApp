﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using ListApp.BusinessObjects;
using ListApp.Services;
using System;
using System.Threading.Tasks;

namespace ListApp
{
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
	public class LoginActivity : AppCompatActivity
	{
		AuthenticationService _authenticationService;

		public LoginActivity()
		{
			_authenticationService = ContainerFactory.Get<AuthenticationService>();
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.activity_main);

			var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
			SetSupportActionBar(toolbar);

			var button = FindViewById<Button>(Resource.Id.btnLogin);
			button.Click += async (sender, e) => await LoginOnClickAsync(sender, e);
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.menu_main, menu);
			return true;
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			int id = item.ItemId;
			if (id == Resource.Id.action_settings)
			{
				return true;
			}

			return base.OnOptionsItemSelected(item);
		}

		private async Task LoginOnClickAsync(object sender, EventArgs eventArgs)
		{
			var fldCodiceFiscale = FindViewById<TextView>(Resource.Id.fldCodiceFiscale);
			var fldUsername = FindViewById<TextView>(Resource.Id.fldUsername);
			var fldPassword = FindViewById<TextView>(Resource.Id.fldPassword);
			var credentials = new UserCredentials() { CfPersona = fldCodiceFiscale.Text, Username = fldUsername.Text, Password = fldPassword.Text };

			if (await _authenticationService.LogonAsync(credentials))
			{
				View view = (View)sender;
				var intent = new Intent(this, typeof(DocumentsListActivity));
				StartActivity(intent);
			}
		}
	}
}