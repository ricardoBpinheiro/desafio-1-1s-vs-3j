using Newtonsoft.Json;

namespace ApiDesafioUsers.Models.Responses
{
    public class ResponseEvaluation
    {
        [JsonProperty("tested_endpoints")]
        public List<EndPointTest> TestedEndpoints { get; set; } = [];

        public ResponseEvaluation()
        {
        }
    }

    public class EndPointTest
    {
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("status")]
        public int Status { get; set; } = 400;

        [JsonProperty("time_ms")]
        public double Timems { get; set; } = 0.0;

        [JsonProperty("valid_response")]
        public bool ValidResponse { get; set; } = false;

        public EndPointTest()
        {
        }
    }
}
