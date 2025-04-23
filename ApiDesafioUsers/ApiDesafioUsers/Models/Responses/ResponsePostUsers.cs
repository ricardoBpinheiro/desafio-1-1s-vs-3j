using Newtonsoft.Json;

namespace ApiDesafioUsers.Models.Responses;

public class ResponsePostUsers
{
    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("user_count")]
    public int UserCount { get; set; }

    public ResponsePostUsers(string message, int count)
    {
        Message = message;
        UserCount = count;
    }
}
