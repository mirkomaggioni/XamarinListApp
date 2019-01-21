using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Views;
using Android.Widget;
using ListApp.BusinessObjects;

namespace ListApp.Adapters
{
	class PersonDocumentAdapter : BaseAdapter<PersonDocument>
	{
		private readonly List<PersonDocument> _printModel;
		private readonly LayoutInflater _inflater;

		public PersonDocumentAdapter(LayoutInflater inflater, IEnumerable<PersonDocument> persons)
		{
			_printModel = persons.ToList();
			_inflater = inflater;
		}

		public override long GetItemId(int position) => position;

		public override PersonDocument this[int position] => _printModel[position];

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var view = convertView;
			var person = _printModel[position];

			if (view == null)
				view = _inflater.Inflate(Resource.Layout.document_item, null);

			view.FindViewById<TextView>(Resource.Id.listItemYear).Text = person.Anno.ToString();
			view.FindViewById<TextView>(Resource.Id.listItemDescription).Text = person.Descrizione;

			return view;
		}

		public override int Count => _printModel.Count();
	}
}