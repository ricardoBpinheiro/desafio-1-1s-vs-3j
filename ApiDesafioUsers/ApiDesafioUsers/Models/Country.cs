using Newtonsoft.Json;

namespace ApiDesafioUsers.Models
{
    public class Country
    {
        [JsonProperty("country")]
        public string CountryName { get; set; } = string.Empty;

        [JsonProperty("total")]
        public int Total { get; set; }

        public Country()
        {
        }

    }
}
