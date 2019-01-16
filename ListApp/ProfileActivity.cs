using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;

namespace ListApp
{
	[Activity(Label = "Profile", Theme = "@style/AppTheme.NoActionBar")]
	public class ProfileActivity : Fragment
	{
		public static ProfileActivity NewInstance(int? id = null)
		{
			var bundle = new Bundle();

			if (id.HasValue)
				bundle.PutInt("id", id.Value);
			return new ProfileActivity { Arguments = bundle };
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			return inflater.Inflate(Resource.Layout.profile, container, false);
		}
	}
}