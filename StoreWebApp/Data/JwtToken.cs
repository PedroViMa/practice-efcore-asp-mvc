using Newtonsoft.Json;

namespace StoreWebApp.Data
{
    public class JwtToken
    {
        [JsonProperty("access_token")]
        public string? AccessToken { get; set; }

        [JsonProperty("expires_at")]
        public DateTime ExpireDate { get; set; }
    }
}
