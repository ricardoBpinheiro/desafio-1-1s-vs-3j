using Newtonsoft.Json;

namespace ApiDesafioUsers.Models;

public class User : UserModel
{
    [JsonProperty("equipe")]
    public UserTeam Equipe { get; set; }

    [JsonProperty("logs")]
    public List<UserLog> Logs { get; set; }

    public User()
    {
        Equipe = new UserTeam();
        Logs = [];
    }
}

public class UserModel
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("nome")]
    public string Nome { get; set; } = string.Empty;

    [JsonProperty("idade")]
    public int Idade { get; set; }

    [JsonProperty("score")]
    public int Score { get; set; }

    [JsonProperty("ativo")]
    public bool Ativo { get; set; }

    [JsonProperty("pais")]
    public string Pais { get; set; } = string.Empty;

    public UserModel()
    {
    }
}


