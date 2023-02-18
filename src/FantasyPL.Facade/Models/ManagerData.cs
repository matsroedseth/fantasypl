using System.Text.Json.Serialization;

namespace FantasyPL.Facade.Models;

public class Manager
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("player_first_name")]
    public string FirstName { get; set; }

    [JsonPropertyName("player_last_name")]
    public string LastName { get; set; }

    [JsonPropertyName("summary_overall_points")]
    public int OverallPoints { get; set; }

    [JsonPropertyName("summary_overall_rank")]
    public int OverallRank { get; set; }

    [JsonPropertyName("current_event")]
    public int CurrentGameWeek { get; set; }

    [JsonPropertyName("summary_event_points")]
    public int GameWeekPoints { get; set; }

    [JsonPropertyName("summary_event_rank")]
    public int GameWeekRank { get; set; }

    [JsonPropertyName("name")]
    public string TeamName { get; set; }
}