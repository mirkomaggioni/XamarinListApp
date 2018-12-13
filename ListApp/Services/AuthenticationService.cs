using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ListApp.BusinessObjects;
using Newtonsoft.Json;
using Xamarin.Android.Net;

namespace ListApp.Services
{
	public class AuthenticationService
	{
		public string _baseUrl;
		public string CfPersona { get; private set; }
		public Token Token { get; private set; }

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

			using (var response = await client.PostAsync("/oauth/token", content))
			{
				if (response.IsSuccessStatusCode)
				{
					CfPersona = credentials.CfPersona;
					Token = JsonConvert.DeserializeObject<Token>(await response.Content.ReadAsStringAsync());
				}

				return response.IsSuccessStatusCode;
			}
		}
	}
}