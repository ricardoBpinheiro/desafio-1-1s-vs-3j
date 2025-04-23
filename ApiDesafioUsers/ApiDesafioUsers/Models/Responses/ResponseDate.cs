using Newtonsoft.Json;

namespace ApiDesafioUsers.Models.Responses;

public class ResponseDate : ResponseModel
{
    [JsonProperty("logins")]
    public List<LogDia> Logins { get; set; } = [];

    public ResponseDate()
    {
    }
}