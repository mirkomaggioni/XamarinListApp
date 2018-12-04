using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace ListApp
{
	[Activity(Label = "PersonDetail", Theme = "@style/AppTheme.NoActionBar")]
	public class PersonDetail : AppCompatActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.person_detail);

			var detailToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.detailToolbar);
			detailToolbar.Title = "Detail";
			SetSupportActionBar(detailToolbar);
			SupportActionBar.SetDisplayHomeAsUpEnabled(true);
			SupportActionBar.SetHomeButtonEnabled(true);

			var fldFirstname = FindViewById<TextView>(Resource.Id.detailItemFirstname);
			fldFirstname.Text = Intent.GetStringExtra("Firstname");
			var fldLastname = FindViewById<TextView>(Resource.Id.detailItemLastname);
			fldLastname.Text = Intent.GetStringExtra("Lastname");
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			if (item.ItemId == Android.Resource.Id.Home)
				Finish();

			return base.OnOptionsItemSelected(item);
		}
	}
}