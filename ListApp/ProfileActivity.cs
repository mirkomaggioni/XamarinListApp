using Android.App;
using Android.OS;
using Android.Support.V7.App;

namespace ListApp
{
	[Activity(Label = "Profile", Theme = "@style/AppTheme.NoActionBar")]
	public class ProfileActivity : AppCompatActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.profile);
		}
	}
}