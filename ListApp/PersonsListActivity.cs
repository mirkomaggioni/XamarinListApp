using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
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
	[Activity(Label = "PersonsListActivity")]
	public class PersonsListActivity : Activity
	{
		private readonly AuthenticationService _authenticationService;
		private List<PersonDocument> personDocuments;

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

		public PersonsListActivity()
		{
			_authenticationService = ContainerFactory.Get<AuthenticationService>();
		}

		protected override async void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.persons_list);

			var client = new HttpClient(new AndroidClientHandler());
			client.BaseAddress = new Uri(ContainerFactory.Settings.ServerAppUrl);

			var request = new HttpRequestMessage(HttpMethod.Get, $"/odata/Sin/DocumentiPersona?$filter=Persona/CfPersona eq '{_authenticationService.CfPersona}'&$select=Descrizione,Anno");
			request.Headers.Add("Authorization", $"Bearer {_authenticationService.Token.access_token}");

			using (var response = await client.SendAsync(request))
			{
				if (response.IsSuccessStatusCode)
				{
					personDocuments = JsonConvert.DeserializeObject<List<PersonDocument>>(await response.Content.ReadAsStringAsync());
					var list = FindViewById<ListView>(Resource.Id.listPersons);
					list.Adapter = new PersonAdapter(this, persons);
					list.ItemClick += ItemOnClick;
				}
			}
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