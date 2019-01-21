using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using ListApp.Adapters;
using ListApp.BusinessObjects;
using ListApp.Services;
using Newtonsoft.Json;
using Xamarin.Android.Net;

namespace ListApp
{
	[Activity(Label = "DocumentsListActivity")]
	public class DocumentsListActivity : Fragment
	{
		private readonly AuthenticationService _authenticationService;
		private ODataResult<IEnumerable<PersonDocument>> documents;

		public DocumentsListActivity()
		{
			_authenticationService = ContainerFactory.Get<AuthenticationService>();
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			var view = inflater.Inflate(Resource.Layout.documents_list, container, false);
			string content = null;

			Task.Run(async () =>
			{
				var client = new HttpClient(new AndroidClientHandler());
				client.BaseAddress = new Uri(ContainerFactory.Settings.ServerAppUrl);

				var request = new HttpRequestMessage(HttpMethod.Get, $"/odata/Sin/DocumentiPersona?$filter=Persona/CfPersona eq '{_authenticationService.CfPersona}'&$select=Descrizione,Anno");
				request.Headers.Add("Authorization", $"Bearer {_authenticationService.Token.access_token}");
				request.Headers.Add("Accept", "application/json");

				using (var response = await client.SendAsync(request))
				{
					if (response.IsSuccessStatusCode)
					{
						content = await response.Content.ReadAsStringAsync();
						documents = JsonConvert.DeserializeObject<ODataResult<IEnumerable<PersonDocument>>>(content);
						var listView = view.FindViewById<ListView>(Resource.Id.listDocuments);
						listView.Adapter = new PersonDocumentAdapter(inflater, documents.value);
						listView.ItemClick += ItemOnClick;
					}
				}
			}).Wait();

			return view;
		}

		private void ItemOnClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			//var intent = new Intent(this, typeof(DocumentDetailActivity));
			//intent.PutExtra("Year", documents.value.ElementAt(e.Position).Anno.ToString());
			//intent.PutExtra("Description", documents.value.ElementAt(e.Position).Descrizione);
			//StartActivity(intent);
		}
	}
}