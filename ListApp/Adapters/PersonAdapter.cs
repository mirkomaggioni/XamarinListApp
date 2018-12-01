using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Views;
using Android.Widget;
using ListApp.BusinessObjects;

namespace ListApp.Adapters
{
	class PersonAdapter : BaseAdapter<Person>
	{
		Activity context;
		private readonly List<Person> _persons;

		public PersonAdapter(Activity context, List<Person> persons)
		{
			this.context = context;
			_persons = persons;
		}

		public override long GetItemId(int position) => position;

		public override Person this[int position] => _persons[position];

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var view = convertView;
			var person = _persons[position];

			if (view == null)
				view = context.LayoutInflater.Inflate(Resource.Layout.person_item, null);

			view.FindViewById<TextView>(Resource.Id.listItemFirstname).Text = person.Firstname;
			view.FindViewById<TextView>(Resource.Id.listItemLastname).Text = person.Lastname;

			return view;
		}

		public override int Count => _persons.Count();
	}
}