using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json.Nodes;

namespace CharitySL.UI.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
		private readonly HttpClient httpClient;
		private readonly ISyncLocalStorageService localStorage;

		public CustomAuthenticationStateProvider(HttpClient _httpClient, ISyncLocalStorageService localStorage)
		{
			httpClient = _httpClient;
			this.localStorage = localStorage;

			var accessToken = localStorage.GetItem<string>("accessToken");
			if (!string.IsNullOrWhiteSpace(accessToken))
			{
				httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
			}
		}

		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			var user = new ClaimsPrincipal(new ClaimsIdentity());

			try
			{
				var response = await httpClient.GetAsync("manage/info");

				if (response.IsSuccessStatusCode)
				{
					var strResponse = await response.Content.ReadAsStringAsync();
					var jsonResponse = JsonNode.Parse(strResponse);
					var email = jsonResponse["email"].ToString();

					var userDetailsResponse = await httpClient.GetAsync($"api/user/email/{email}");
					if (userDetailsResponse.IsSuccessStatusCode)
					{
						var strUserDetailsResp = await userDetailsResponse.Content.ReadAsStringAsync();
						var userDetailsJsonResponse = JsonNode.Parse(strUserDetailsResp);
						var userId = userDetailsJsonResponse["id"];
						var name = userDetailsJsonResponse["firstName"] + " " + userDetailsJsonResponse["lastName"];
						var guid = userDetailsJsonResponse["guid"];

						this.localStorage.SetItem("userId", userId);
						this.localStorage.SetItem("guid", guid);

						//add email to claims
						var claims = new List<Claim>
						{
							new(ClaimTypes.Name, name),
							new(ClaimTypes.Email, email)
						};

						//set the principal
						var identity = new ClaimsIdentity(claims, "Token");
						user = new ClaimsPrincipal(identity);
						return new AuthenticationState(user);
					}
				}
			}
			catch (Exception ex)
			{

			}

			return new AuthenticationState(user);
		}

		public async Task<FormResult> Login(string email, string password)
		{
			try
			{
				if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
				{
					return new FormResult { Succeeded = false, Errors = ["Email and password are required"] };
				}

				var response = await httpClient.PostAsJsonAsync("login", new { email, password });
				if (response.IsSuccessStatusCode)
				{
					var strResponse = await response.Content.ReadAsStringAsync();
					var jsonResponse = JsonNode.Parse(strResponse);
					var accessToken = jsonResponse["accessToken"].ToString();
					var refreshToken = jsonResponse["refreshToken"].ToString();

					this.localStorage.SetItem("accessToken", accessToken);
					this.localStorage.SetItem("refreshToken", refreshToken);

					httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

					NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
					return new FormResult { Succeeded = true };
				}
				else
				{
					return new FormResult { Succeeded = false, Errors = ["Bad email or password"] };
				}
			}
			catch (Exception ex)
			{
				return new FormResult { Succeeded = false, Errors = ["Connection error"] };
			}
		}

		public async Task Logout()
		{
			localStorage.RemoveItem("accessToken");
			localStorage.RemoveItem("refreshToken");
			localStorage.RemoveItem("userId");
			localStorage.RemoveItem("guid");
			httpClient.DefaultRequestHeaders.Authorization = null;
			NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
		}
	}

	public class FormResult
	{
		public bool Succeeded { get; set; }
		public string[] Errors { get; set; } = [];
	}
}

