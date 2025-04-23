using Newtonsoft.Json;

namespace ApiDesafioUsers.Models;

public class LogDia
{
    [JsonProperty("date")]
    public DateTime Date { get; set; }

    [JsonProperty("total")]
    public int Total { get; set; }

    public LogDia()
    {
    }

}
