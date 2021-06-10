using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Ray.Notification.Common.Services
{
    public class LoginService
    {
        public AuthorizationModel GetAuthenticationToken(string username, string password)
        {
            var parameters = new Dictionary<string, string>
            {
                {"grant_type", "password"},
                {"client_id", ConfigurationManager.AppSettings["idp_id"]},
                {"client_secret", ConfigurationManager.AppSettings["idp_secret"]},
                {"scope", ConfigurationManager.AppSettings["idp_scope"]},
                {"username", username},
                {"password", password}
            };

            var url = ConfigurationManager.AppSettings["idp_url"] + "/connect/token";

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync(url, new FormUrlEncodedContent(parameters)).GetAwaiter().GetResult();
                    var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    var authentication = JsonConvert.DeserializeObject<AuthorizationModel>(result);

                    return authentication;
                }
            }
            catch
            {
                return null;
            }
        }

        public class AuthorizationModel
        {
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }

            [JsonProperty("expires_in")]
            public int Expiry { get; set; }

            [JsonProperty("token_type")]
            public string TokenType { get; set; }

            [JsonProperty("refresh_token")]
            public string RefreshToken { get; set; }

            [JsonProperty("login_state")]
            public int LoginState { get; set; }
        }
    }
}
