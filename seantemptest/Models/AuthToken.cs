using Newtonsoft.Json;

namespace seantemptest.Models
{
    public class AuthToken
    {
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}