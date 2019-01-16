using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using ListApp.Services;

namespace ListApp
{
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
	{
		AuthenticationService _authenticationService;

		public MainActivity()
		{
			_authenticationService = ContainerFactory.Get<AuthenticationService>();
		}

		protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

			var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.appbar);
            SetSupportActionBar(toolbar);

			DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
			ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
			drawer.AddDrawerListener(toggle);
			toggle.SyncState();

			NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
			navigationView.SetNavigationItemSelectedListener(this);
		}

		public bool OnNavigationItemSelected(IMenuItem menuItem)
		{
			int id = menuItem.ItemId;

			if (id == Resource.Id.nav_profile)
			{
				var intent = new Intent(this, typeof(ProfileActivity));
				StartActivity(intent);
			}
			else if (id == Resource.Id.nav_documents)
			{
				var intent = new Intent(this, typeof(DocumentsListActivity));
				StartActivity(intent);
			}

			return true;
		}
	}
}
