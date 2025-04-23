using Newtonsoft.Json;

namespace ApiDesafioUsers.Models;

public class UserTeam
{
    [JsonProperty("nome")]
    public string Nome { get; set; } = string.Empty;

    [JsonProperty("lider")]
    public bool Lider { get; set; } = false;

    [JsonProperty("projetos")]
    public List<TeamProject> Projetos { get; set; }

    [JsonIgnore()]
    public double PorcentagemProjetos
    {
        get
        {
            var projetosConcluidos = Projetos.Count(e => e.Concluido);
            return Projetos.Count > 0 ? projetosConcluidos / Convert.ToDouble(Projetos.Count) : 0.0;
        }
    }

    public UserTeam()
    {
        Projetos = [];
    }
}
