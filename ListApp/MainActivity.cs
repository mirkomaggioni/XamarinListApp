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
			//navigationView.SetNavigationItemSelectedListener(this);
		}

		private void SetupDrawerContent(NavigationView navigationView)
		{
			navigationView.NavigationItemSelected += (sender, e) =>
			{
				e.MenuItem.SetChecked(true);

				FragmentTransaction ft = this.FragmentManager.BeginTransaction();

				switch (e.MenuItem.ItemId)
				{
					case Resource.Id.nav_profile:
						var act = ProfileActivity.NewInstance();
						ft.Replace(Resource.Id.content_frame, act);
						break;
				}

				ft.Commit();
				drawerLayout.CloseDrawers();
			};
		}

		//public bool OnNavigationItemSelected(IMenuItem menuItem)
		//{
		//	int id = menuItem.ItemId;

		//	if (id == Resource.Id.nav_profile)
		//	{
		//		var container = FindViewById(Resource.Id.content_frame);
		//		var quoteFrag = ProfileActivity.NewInstance();

		//		FragmentTransaction ft = FragmentManager.BeginTransaction();
		//		ft.Replace(Resource.Id.content_frame, quoteFrag);
		//		ft.AddToBackStack(null);
		//		ft.SetTransition(FragmentTransit.FragmentFade);
		//		ft.Commit();

		//		//var intent = new Intent(this, typeof(ProfileActivity));

		//		//StartActivity(intent);
		//	}
		//	else if (id == Resource.Id.nav_documents)
		//	{
		//		var intent = new Intent(this, typeof(DocumentsListActivity));
		//		StartActivity(intent);
		//	}

		//	return true;
		//}
	}
}
