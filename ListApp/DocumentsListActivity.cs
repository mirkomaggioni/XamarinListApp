using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using ListApp.Adapters;
using ListApp.BusinessObjects;
using ListApp.Services;
using Newtonsoft.Json;
using Xamarin.Android.Net;

namespace ListApp
{
	[Activity(Label = "DocumentsListActivity")]
	public class DocumentsListActivity : Activity
	{
		private readonly AuthenticationService _authenticationService;
		private ODataResult<IEnumerable<PersonDocument>> documents;

		public DocumentsListActivity()
		{
			_authenticationService = ContainerFactory.Get<AuthenticationService>();
		}

		protected override async void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.documents_list);

			var client = new HttpClient(new AndroidClientHandler());
			client.BaseAddress = new Uri(ContainerFactory.Settings.ServerAppUrl);

			var request = new HttpRequestMessage(HttpMethod.Get, $"/odata/Sin/DocumentiPersona?$filter=Persona/CfPersona eq '{_authenticationService.CfPersona}'&$select=Descrizione,Anno");
			request.Headers.Add("Authorization", $"Bearer {_authenticationService.Token.access_token}");
			request.Headers.Add("Accept", "application/json");

			using (var response = await client.SendAsync(request))
			{
				if (response.IsSuccessStatusCode)
				{
					documents = JsonConvert.DeserializeObject<ODataResult<IEnumerable<PersonDocument>>>(await response.Content.ReadAsStringAsync());
					var list = FindViewById<ListView>(Resource.Id.listDocuments);
					list.Adapter = new PersonDocumentAdapter(this, documents.value);
					list.ItemClick += ItemOnClick;
				}
			}
		}

		private void ItemOnClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			var intent = new Intent(this, typeof(DocumentDetailActivity));
			intent.PutExtra("Year", documents.value.ElementAt(e.Position).Anno.ToString());
			intent.PutExtra("Description", documents.value.ElementAt(e.Position).Descrizione);
			StartActivity(intent);
		}
	}
}