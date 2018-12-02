using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using ListApp.Adapters;
using ListApp.BusinessObjects;

namespace ListApp
{
	[Activity(Label = "PersonsListActivity")]
	public class PersonsListActivity : Activity
	{
		private List<Person> persons = new List<Person>() {
			new Person()
			{
				Firstname = "Mirko",
				Lastname = "Maggioni"
			},
			new Person()
			{
				Firstname = "Davide",
				Lastname = "Varini"
			},
			new Person()
			{
				Firstname = "Paolo",
				Lastname = "Zacchi"
			}
		};

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.persons_list);

			var list = FindViewById<ListView>(Resource.Id.listPersons);
			list.Adapter = new PersonAdapter(this, persons);
			list.ItemClick += ItemOnClick;
		}

		private void ItemOnClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			var intent = new Intent(this, typeof(PersonDetail));
			intent.PutExtra("Firstname", persons[e.Position].Firstname);
			intent.PutExtra("Lastname", persons[e.Position].Lastname);
			StartActivity(intent);
		}
	}
}