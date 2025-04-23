using Newtonsoft.Json;

namespace ApiDesafioUsers.Models.Responses;

public class ResponseTeam : ResponseModel
{
    [JsonProperty("teams")]
    public List<Equipe> Teams { get; set; } = [];

    public ResponseTeam()
    {
    }
}
