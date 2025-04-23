using Newtonsoft.Json;

namespace ApiDesafioUsers.Models.Responses;

public class ResponseSuperUsers : ResponseModel
{
    [JsonProperty("data")]
    public List<UserModel> Data { get; set; }

    public ResponseSuperUsers()
    {
        Data = [];
    }
}
