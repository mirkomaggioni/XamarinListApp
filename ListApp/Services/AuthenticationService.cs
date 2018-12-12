using ListApp.BusinessObjects;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Android.Net;

namespace ListApp.Services
{
	public class AuthenticationService
	{
		private readonly string _baseUrl;

		public AuthenticationService(string baseUrl)
		{
			_baseUrl = baseUrl;
		}

		public async Task<bool> LogonAsync(UserCredentials credentials)
		{
			var client = new HttpClient(new AndroidClientHandler());
			client.BaseAddress = new System.Uri(_baseUrl);

			var content = new FormUrlEncodedContent(new[]
			{
				new KeyValuePair<string, string>("grant_type", "password"),
				new KeyValuePair<string, string>("dominio", "Lombardia"),
				new KeyValuePair<string, string>("username", credentials.Username),
				new KeyValuePair<string, string>("password", credentials.Password)
			});

			var response = await client.PostAsync("/oauth/token", content);

			return response.IsSuccessStatusCode;
		}
	}
}