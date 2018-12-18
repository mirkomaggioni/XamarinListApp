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
		Activity context;
		private readonly List<PersonDocument> _printModel;

		public PersonDocumentAdapter(Activity context, IEnumerable<PersonDocument> persons)
		{
			this.context = context;
			_printModel = persons.ToList();
		}

		public override long GetItemId(int position) => position;

		public override PersonDocument this[int position] => _printModel[position];

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var view = convertView;
			var person = _printModel[position];

			if (view == null)
				view = context.LayoutInflater.Inflate(Resource.Layout.document_item, null);

			view.FindViewById<TextView>(Resource.Id.listItemYear).Text = person.Anno.ToString();
			view.FindViewById<TextView>(Resource.Id.listItemDescription).Text = person.Descrizione;

			return view;
		}

		public override int Count => _printModel.Count();
	}
}