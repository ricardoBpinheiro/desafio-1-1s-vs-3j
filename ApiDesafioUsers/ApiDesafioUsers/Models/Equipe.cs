using Newtonsoft.Json;

namespace ApiDesafioUsers.Models;

public class Equipe
{
    [JsonProperty("team")]
    public string Team { get; set; } = string.Empty;

    [JsonProperty("total_members")]
    public int TotalMembers { get; set; }

    [JsonProperty("leaders")]
    public int Leaders { get; set; }

    [JsonProperty("completed_projects")]
    public int CompletedProjects { get; set; }
    
    [JsonProperty("active_percentage")]
    public double ActivePercentage { get; set; }

    public Equipe()
    {
    }
}
