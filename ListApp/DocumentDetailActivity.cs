using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace ListApp
{
	[Activity(Label = "DocumentDetail", Theme = "@style/AppTheme.NoActionBar")]
	public class DocumentDetailActivity : AppCompatActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.document_detail);

			var detailToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.detailToolbar);
			detailToolbar.Title = "Detail";
			SetSupportActionBar(detailToolbar);
			SupportActionBar.SetDisplayHomeAsUpEnabled(true);
			SupportActionBar.SetHomeButtonEnabled(true);

			var fldYear = FindViewById<TextView>(Resource.Id.detailItemYear);
			fldYear.Text = Intent.GetStringExtra("Year");
			var fldDescription = FindViewById<TextView>(Resource.Id.detailItemDescription);
			fldDescription.Text = Intent.GetStringExtra("Description");
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			if (item.ItemId == Android.Resource.Id.Home)
				Finish();

			return base.OnOptionsItemSelected(item);
		}
	}
}