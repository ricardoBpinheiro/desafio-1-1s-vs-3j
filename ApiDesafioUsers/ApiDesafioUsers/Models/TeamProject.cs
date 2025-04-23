using Newtonsoft.Json;

namespace ApiDesafioUsers.Models
{
    public class TeamProject
    {
        [JsonProperty("nome")]
        public string Nome { get; set; } = string.Empty;

        [JsonProperty("concluido")]
        public bool Concluido { get; set; }

        public TeamProject()
        {
        }
    }
}
