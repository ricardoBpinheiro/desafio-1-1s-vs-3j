using Newtonsoft.Json;

namespace ApiDesafioUsers.Models;

public class UserLog
{
    [JsonProperty("data")]
    public DateTime Data { get; set; }

    [JsonProperty("acao")]
    public string Acao { get; set; } = string.Empty;

    public UserLog()
    {
    }
}
