using Android.App;
using Android.OS;
using Android.Views;

namespace ListApp
{
	[Activity(Label = "Profile", Theme = "@style/AppTheme.NoActionBar")]
	public class ProfileActivity : Fragment
	{
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			return inflater.Inflate(Resource.Layout.profile, container, false);
		}
	}
}