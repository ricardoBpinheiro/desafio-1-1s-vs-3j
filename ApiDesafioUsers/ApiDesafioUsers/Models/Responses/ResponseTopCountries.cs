using Newtonsoft.Json;

namespace ApiDesafioUsers.Models.Responses;

public class ResponseTopCountries : ResponseModel
{
    [JsonProperty("countries")]
    public List<Country> Countries { get; set; } = [];

    public ResponseTopCountries()
    {
    }
}
