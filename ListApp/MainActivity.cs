using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using ListApp.BusinessObjects;
using ListApp.Services;

namespace ListApp
{
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
		AuthenticationService _authenticationService;

		public MainActivity()
		{
			var serializer = new XmlSerializer(typeof(AppSettings));
			var assembly = IntrospectionExtensions.GetTypeInfo(typeof(AuthenticationService)).Assembly;
			var stream = assembly.GetManifestResourceStream("ListApp.AppSettings.xml");

			using (var reader = new StreamReader(stream))
			{
				var settings = (AppSettings)serializer.Deserialize(reader);
				_authenticationService = new AuthenticationService(settings.ServerAppUrl);
			}
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
			var fldUsername = FindViewById<TextView>(Resource.Id.fldUsername);
			var fldPassword = FindViewById<TextView>(Resource.Id.fldPassword);
			var credentials = new UserCredentials() { Username = fldUsername.Text, Password = fldPassword.Text };

			if (await _authenticationService.LogonAsync(credentials))
			{
				View view = (View)sender;
				var intent = new Intent(this, typeof(PersonsListActivity));
				StartActivity(intent);
			}
        }
	}
}
