using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MyPasswordManager
{
    public class LoginJsonRoot
    {
        [JsonPropertyName("logins")]
        public IList<LoginData> Logins { get; set; }
    }

    public class LoginData
    {
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("website")]
        public string Website { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}