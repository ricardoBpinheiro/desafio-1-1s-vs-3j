using Newtonsoft.Json;

namespace ApiDesafioUsers.Models.Responses
{
    public class ResponseModel
    {
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("execution_time_ms")]
        public double ExecutionTimems { get; set; }

        public ResponseModel()
        {
        }
    }
}
