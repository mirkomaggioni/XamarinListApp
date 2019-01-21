using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using ListApp.Services;

namespace ListApp
{
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class MainActivity : AppCompatActivity
	{
		AuthenticationService _authenticationService;
		private DrawerLayout drawerLayout;

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

			drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
			ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
			drawerLayout.AddDrawerListener(toggle);
			toggle.SyncState();

			NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
			SetupDrawerContent(navigationView);
		}

		private void SetupDrawerContent(NavigationView navigationView)
		{
			navigationView.NavigationItemSelected += (sender, e) =>
			{
				e.MenuItem.SetChecked(true);
				var ft = FragmentManager.BeginTransaction();
				var bundle = new Bundle();
				var act = new Fragment();

				switch (e.MenuItem.ItemId)
				{
					case Resource.Id.nav_profile:
						act = new ProfileActivity();
						break;
					case Resource.Id.nav_documents:
						act = new DocumentsListActivity();
						break;
				}

				ft.Replace(Resource.Id.content_frame, act);
				ft.Commit();
				drawerLayout.CloseDrawers();
			};
		}
	}
}
